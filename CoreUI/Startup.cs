using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeTableLibrary;
using TimeTableLibrary.Helpers;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.RozkladRequests;
using TimeTableLibrary.Mappers.Interfaces;
using TimeTableLibrary.RozkladModels;
using TimeTableLibrary.Mappers.RozkladMappers;
using TimeTableLibrary.FpmModels;
using TimeTableLibrary.Mappers.FpmMappers;

namespace CoreUI
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
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});


			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddSingleton<FpmClient>();
			services.AddSingleton<RozkladClient>();
			services.AddScoped<RozkladSubjectMapper>();
			services.AddScoped<RozkladTeacherMapper>();
			services.AddScoped<FpmSubjectMapper>();
			services.AddScoped<FpmTeacherMapper>();
			services.AddSingleton<MappingService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Authorization}");
			});
		}
	}
}
