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
            services.AddControllers().AddControllersAsServices();//����controller��Ϊ�����һ���ֳ��֣�����controller��ע����autofac�ӹ�

            services.AddAuthorization();//������Ȩ�ܵ��м��
            /**
             * ������֤�ܵ��м��
             * ������ָ�����������֤�ĵ�ַ����Ϊ�����֤�����ǵ����ķ���
             * ApiName��Scope�еİ��������жϵ�ǰ��Token�Ƿ���Է����������
             */
            services.AddAuthentication("Bearer").AddIdentityServerAuthentication(options =>
            {
                options.Authority = "http://192.168.0.101:4090";
                options.RequireHttpsMetadata = false;
                options.ApiName = "SystemService";
            });

            #region ����Gzipѹ������ָ���ٶ����Ĳ���
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

            #region �û�
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserLogic>().As<IUserLogic>().InstancePerLifetimeScope();
            builder.RegisterType<UserMapping>().As<IUserMapping>().InstancePerLifetimeScope();
            #endregion

            #region ��ɫ
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoleLogic>().As<IRoleLogic>().InstancePerLifetimeScope();
            builder.RegisterType<RoleMapping>().As<IRoleMapping>().InstancePerLifetimeScope();
            #endregion

            #region Ȩ��
            builder.RegisterType<PermissionRepository>().As<IPermissionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionLogic>().As<IPermissionLogic>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionMapping>().As<IPermissionMapping>().InstancePerLifetimeScope();
            #endregion

            #region ����
            builder.RegisterType<DepartmentRepository>().As<IDepartmentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DepartmentLogic>().As<IDepartmentLogic>().InstancePerLifetimeScope();
            builder.RegisterType<DepartmentMapping>().As<IDepartmentMapping>().InstancePerLifetimeScope();
            #endregion
        }
    }
}
