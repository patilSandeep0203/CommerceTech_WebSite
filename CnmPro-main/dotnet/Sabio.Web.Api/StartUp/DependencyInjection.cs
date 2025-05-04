using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sabio.Data;
using Sabio.Models.Interfaces;
using Sabio.Services;
using Sabio.Services.Email;
using Sabio.Services.Interfaces;
using Sabio.Web.Api.StartUp.DependencyInjection;
using Sabio.Web.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Sabio.Services.StripeService;
using Sabio.Services.Schedules;
using Sabio.Services.LicenseVerification;
using Sabio.Services.FAQs;
using Sabio.Models;
using Sabio.Services.Comments;
using System.ComponentModel.Design;

namespace Sabio.Web.StartUp
{
    public class DependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration is IConfigurationRoot)
            {
                services.AddSingleton<IConfigurationRoot>(configuration as IConfigurationRoot);   // IConfigurationRoot
            }

            services.AddSingleton<IConfiguration>(configuration);   // IConfiguration explicitly

            string connString = configuration.GetConnectionString("Default");
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2
            // The are a number of differe Add* methods you can use. Please verify which one you
            // should be using services.AddScoped<IMyDependency, MyDependency>();

            // services.AddTransient<IOperationTransient, Operation>();

            // services.AddScoped<IOperationScoped, Operation>();

            // services.AddSingleton<IOperationSingleton, Operation>();

            services.AddSingleton<IAdminDashService, AdminDashService>();

            services.AddSingleton<IAuthenticationService<int>, WebAuthenticationService>();

            services.AddSingleton<IBlogService, BlogService>();

            services.AddSingleton<IContactUsService, ContactUsService>();

            services.AddSingleton<Sabio.Data.Providers.IDataProvider, SqlDataProvider>(delegate (IServiceProvider provider)
            {
                return new SqlDataProvider(connString);
            }
            );

            services.AddSingleton<IEmailServices, EmailServices>();

            services.AddSingleton<IEventService, EventService>();

            services.AddSingleton<IExternalLinkService, ExternalLinkService>();

            services.AddSingleton<IFAQService, FAQService>();

            services.AddSingleton<IFileService, FileService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IIdentityProvider<int>, WebAuthenticationService>();

            services.AddSingleton<IJobService, JobService > ();

            services.AddSingleton<IJobScheduleService, JobScheduleService>();

            services.AddSingleton<ILicenseVerificationService, LicenseVerificationService>();

            services.AddSingleton<ILocationService, LocationService>();

            services.AddSingleton<ILookUpService, LookUpService>();

            services.AddSingleton<IMessageService, MessageService>();

            services.AddSingleton<INewsletterSubscriptionService, NewsletterSubscriptionService>();

            services.AddSingleton<IOrgAdminDashService, OrgAdminDashService>();

            services.AddSingleton<IOrganizationsFollowersServices, OrganizationsFollowersServices>();

            services.AddSingleton<IOrganizationService, OrganizationService>();

            services.AddSingleton<IProposalService, ProposalService>();

            services.AddSingleton<IRatingsService, RatingsService>();

            services.AddSingleton<IResourceCategoriesService, ResourceCategoriesService>();

            services.AddSingleton<IResourcesService, ResourcesService>();

            services.AddSingleton<IResumesService, ResumesService>();

            services.AddSingleton<IScheduleService, ScheduleService>();

            services.AddSingleton<IShareStoryService, ShareStoryService>();

            services.AddSingleton<ISiteReferenceService, SiteReferenceService>();

            services.AddSingleton<IStripeService, StripeService>();

            services.AddSingleton<ISurveyService, SurveyService>();
           
            services.AddSingleton<ITimesheetService, TimesheetService>();

            services.AddSingleton<IUserProfileMapper, UserProfileMapperService>();

            services.AddSingleton<IUserProfilesService, UserProfilesService>();

            services.AddSingleton<IUserService, UserService>();

            services.AddSingleton<ICommentService, CommentService>();


            GetAllEntities().ForEach(tt =>
            {
                IConfigureDependencyInjection idi = Activator.CreateInstance(tt) as IConfigureDependencyInjection;

                //This will not error by way of being null. BUT if the code within the method does
                // then we would rather have the error loadly on startup then worry about debuging the issues as it runs
                idi.ConfigureServices(services, configuration);
            });
        }

        public static List<Type> GetAllEntities()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                 .Where(x => typeof(IConfigureDependencyInjection).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                 .ToList();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}