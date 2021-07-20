using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Portal.Application.Hashers;
using Portal.Application.Interfaces;
using Portal.Application.MapperProfiles;
using Portal.Application.ModelsDTO;
using Portal.Application.Services;
using Portal.Domain.Identity;
using Portal.Domain.Interfaces;
using Portal.EfCore.Context;
using Portal.Infrastructure.Repositories;
using Portal.WebUI.MapperProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.WebUI
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
            services.AddControllersWithViews();

            services.AddDbContext<PortalDbContext>(c =>
                    c.UseSqlServer(Configuration.GetConnectionString("EducationPortal")));

            services.AddScoped<DbContext>(c => c.GetRequiredService<PortalDbContext>());
            services.AddScoped(typeof(IEfRepository<>), typeof(EfCoreRepository<>));

            services.AddTransient<IHasher, SHA256Hasher>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IMaterialsService, MaterialsService>();
            services.AddScoped<IInternetMaterialService, InternetMaterialService>();
            services.AddScoped<IVideoMaterialService, VideoMaterialService>();
            services.AddScoped<ITextMaterialService, TextMaterialService>();
            services.AddScoped<ICourseSkillService, CourseSkillService>();

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<PortalDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });

            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(SkillViewModelProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
