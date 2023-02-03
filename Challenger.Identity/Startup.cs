﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Challenger.Email.Templates;
using Challenger.Identity.Migrations.IdentityServer.IdentityDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuantumCore.Email;
using QuantumCore.Email.Builders;
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


            services.Configure<EmailSettings>(Configuration.GetSection("Email"));

            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
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
                options.Discovery.CustomEntries.Add("challenger", "localhost:5002");
                options.Discovery.CustomEntries.Add("challenger_web", "localhost:4002");
                //options.Authentication = new AuthenticationOptions()
                //{
                //    CookieLifetime = TimeSpan.FromHours(10), // ID server cookie timeout set to 10 hours
                //    CookieSlidingExpiration = true
                //};

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
            .AddAspNetIdentity<ApplicationUser>();
            //.AddTestUsers(TestUsers.Users);

            if (!Environment.IsDevelopment())
            {
                builder.AddSigningCredential("someName"); //configure for production!
            }
            else
            {
                builder.AddDeveloperSigningCredential();
            }

            builder.Services.AddScoped<IEmailSender, QuantumEmailSender>();
            builder.Services.AddScoped<EmailBuilder, ChallengerEmailBuilder>();

            //local calls to IS
            services.AddLocalApiAuthentication();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IdentityContext identityContext)
        {
            identityContext.Database.Migrate();

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
