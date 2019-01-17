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
using TimeTableLibrary.Client;
using TimeTableLibrary.FpmModels;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.Mappers;
using TimeTableLibrary.RozkladModels;
using TimeTableLibrary.RozkladRequests;

namespace TimeTableUI
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
			
			//services.AddSingleton<IFpmClient, FpmClient>();
			//services.AddSingleton<IRozkladClient, RozkladClient>();

			//services.AddTransient<IElementsMapper<RozkladTeacher, FpmTeacher>,TeachersMapper>();
			//services.AddTransient<IElementsMapper<RozkladSubject, FpmSubject>, SubjectsMapper>();
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
					template: "{controller=Login}/{action=Index}/{id?}");
			});
		}
	}
}
