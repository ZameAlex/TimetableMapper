using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeTableLibrary.Helpers;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.RozkladRequests;
using TimeTableLibrary.Mappers.Interfaces;
using TimeTableLibrary.RozkladModels;
using TimeTableLibrary.Mappers.RozkladMappers;
using TimeTableLibrary.FpmModels;
using TimeTableLibrary.Mappers.FpmMappers;
using TimeTableLibrary.Helpers.Local;
using TimeTableLibrary.Helpers.Interfaces;
using TimeTableLibrary.Helpers.Models;
using CoreUI.Services.Interfaces;
using CoreUI.Services.Implementation;

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
			services.AddOptions();
			services.Configure<DefaultFiles>(Configuration.GetSection("DefaultFiles"));
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddSingleton<FpmClient>();
			services.AddSingleton<RozkladClient>();
			services.AddSingleton<IMapper<string, RozkladSubject>, RozkladSubjectMapper>();
			services.AddSingleton<IMapper<string, RozkladTeacher>, RozkladTeacherMapper>();
			services.AddSingleton<IMapper<string, FpmSubject>, FpmSubjectMapper>();
			services.AddSingleton<IMapper<string, FpmTeacher>, FpmTeacherMapper>();
			services.AddSingleton<IReader, LocalReader>();
			services.AddSingleton<IWriter, LocalWriter>();
			services.AddSingleton<SetService<FpmGroup>, SetSubjectsService>();
			services.AddSingleton<SetService<FpmTeacher>, SetTeacherService>();
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
