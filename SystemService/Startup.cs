using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace SystemService
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
            services.AddHttpContextAccessor();
            services.AddControllers().AddControllersAsServices();//配置controller作为服务的一部分出现，这样controller的注入由autofac接管

            services.AddAuthorization();//加入授权管道中间件
            /**
             * 加入认证管道中间件
             * 参数是指定进行身份认证的地址，因为身份认证服务是单独的服务
             * ApiName是Scope中的包含，来判断当前的Token是否可以访问这个服务
             */
            services.AddAuthentication("Bearer").AddIdentityServerAuthentication(options =>
            {
                options.Authority = "http://192.168.0.101:4090";
                options.RequireHttpsMetadata = false;
                options.ApiName = "SystemService";
            });

            #region 开启Gzip压缩，并指定速度最快的策略
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseResponseCompression();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<BaseUnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            #region 用户
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserLogic>().As<IUserLogic>().InstancePerLifetimeScope();
            builder.RegisterType<UserMapping>().As<IUserMapping>().InstancePerLifetimeScope();
            #endregion

            #region 角色
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoleLogic>().As<IRoleLogic>().InstancePerLifetimeScope();
            builder.RegisterType<RoleMapping>().As<IRoleMapping>().InstancePerLifetimeScope();
            #endregion

            #region 权限
            builder.RegisterType<PermissionRepository>().As<IPermissionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionLogic>().As<IPermissionLogic>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionMapping>().As<IPermissionMapping>().InstancePerLifetimeScope();
            #endregion

            #region 部门
            builder.RegisterType<DepartmentRepository>().As<IDepartmentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DepartmentLogic>().As<IDepartmentLogic>().InstancePerLifetimeScope();
            builder.RegisterType<DepartmentMapping>().As<IDepartmentMapping>().InstancePerLifetimeScope();
            #endregion
        }
    }
}
