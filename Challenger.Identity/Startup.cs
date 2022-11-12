﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Challenger.Identity.Migrations.IdentityServer.IdentityDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Challenger.Identity
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(
            IWebHostEnvironment environment,
            IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddDefaultIdentity<IdentityUser>()//options => options.SignIn.RequireConfirmedAccount = false)
                            .AddEntityFrameworkStores<IdentityContext>()
                            .AddDefaultTokenProviders();

            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.UserInteraction.LoginUrl = "/Account/Login";
                options.UserInteraction.LogoutUrl = "/Account/Logout";

                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            })
            .AddConfigurationStore(opt =>
            {
                opt.ConfigureDbContext = c => c.UseSqlServer(Configuration.GetConnectionString("IdentityConfigurationConnection"),
                    sql => sql.MigrationsAssembly(migrationAssembly));
            })
            .AddOperationalStore(opt =>
            {
                opt.ConfigureDbContext = o => o.UseSqlServer(Configuration.GetConnectionString("IdentityConfigurationConnection"),
                    sql => sql.MigrationsAssembly(migrationAssembly));
            })
            .AddAspNetIdentity<IdentityUser>();
            //.AddTestUsers(TestUsers.Users);

            if (!Environment.IsDevelopment())
            {
                builder.AddSigningCredential("someName"); //configure for production!
            }
            else
            {
                builder.AddDeveloperSigningCredential();
            }


            services.AddControllersWithViews();

            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy(name: "AllowAnyOrigin",
            //                      builder =>
            //                      {
            //                          builder//.WithOrigins("http://54.37.137.86", "https://54.37.137.86", "http://localhost:4200")
            //                          .AllowAnyOrigin()
            //                          .AllowAnyMethod()
            //                          .AllowAnyHeader();
            //                          //.WithHeaders("content-type")
            //                          //.AllowCredentials();
            //                      });
            //});
        }

        public void Configure(IApplicationBuilder app, IdentityContext identityContext)
        {
            identityContext.Database.Migrate();

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // uncomment if you want to add MVC
            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
