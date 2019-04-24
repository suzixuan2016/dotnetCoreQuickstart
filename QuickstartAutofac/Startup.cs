using Autofac;
using Autofac.Extensions.DependencyInjection;
using Bussiness;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using QuickstartAutofac.Filter;
using QuickstartAutofac.Middlewares;
using Swashbuckle.AspNetCore.Swagger;

namespace QuickstartAutofac
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(c =>
                {
                    //针对controller添加全局filter
                    c.Filters.Add(typeof(LogFilter));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            string tokenKey = "TokenKey";

            //添加swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });

                //添加header认证
                c.AddSecurityDefinition(tokenKey, new ApiKeyScheme()
                {
                    Description = "验证Token",
                    Name = tokenKey,
                    In = "header",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { tokenKey, new string[] { } }
                });

                //1,visualstudio启用生成xml注释文件，2.以下代码用于将注释添加到swagger
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "QuickstartAutofac.xml");
                c.IncludeXmlComments(xmlPath);
                c.IgnoreObsoleteActions();
            });

            var builder = new ContainerBuilder();

            builder.Populate(services);
            builder.RegisterModule(new AutofacModule());
            builder.RegisterModule(new BussinessModule());

            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //添加统一异常处理
            app.UseMiddleware<ExceptionHandleMiddleware>();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
