using InteraceDemo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServiceDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserService,UserService>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClientDemo", Version = "v1" });
            });

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:15201";    //id4的地址
                    options.ApiName = "api1";
                    options.RequireHttpsMetadata = false;//如果不使用Https，则需要配置这个
                });
            services.AddAuthorization(options =>
            {
                //基于策略授权
                options.AddPolicy("FindUserPolicy", builder =>
                {
                    //客户端Scope中包含api1.weather.scope才能访问
                    builder.RequireScope("api1.finduser.scope");
                });
                options.AddPolicy("FindAllPolicy", builder =>
                {
                    //客户端Scope中包含api1.test.scope才能访问
                    builder.RequireScope("api1.findall.scope");
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClientDemo v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();    //鉴权
            app.UseAuthorization();     //授权

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //注册Consul
            Configuration.ConsulRegist();

        }
    }
}
