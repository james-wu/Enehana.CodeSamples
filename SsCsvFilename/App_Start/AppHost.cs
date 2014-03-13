using System;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using ServiceStack.Configuration;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.ServiceModel;
using ServiceStack.WebHost.Endpoints;
using ServiceStack.Common.Web;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints.Formats;

[assembly: WebActivator.PreApplicationStartMethod(typeof(SsCsvFilename.App_Start.AppHost), "Start")]


/**
 * Entire ServiceStack Starter Template configured with a 'Hello' Web Service and a 'Todo' Rest Service.
 *
 * Auto-Generated Metadata API page at: /metadata
 * See other complete web service examples at: https://github.com/ServiceStack/ServiceStack.Examples
 */

namespace SsCsvFilename.App_Start
{
	public class AppHost
		: AppHostBase
	{		
		public AppHost() //Tell ServiceStack the name and where to find your web services
			: base("StarterTemplate ASP.NET Host", typeof(HelloService).Assembly) { }

		public override void Configure(Funq.Container container)
		{
			//Set JSON web services to return idiomatic JSON camelCase properties
			ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;
		
			//Configure User Defined REST Paths
			Routes
			  .Add<Hello>("/hello")
			  .Add<Hello>("/hello/{Name*}");

			//Uncomment to change the default ServiceStack configuration
			//SetConfig(new EndpointHostConfig {
			//});

			//Enable Authentication
			//ConfigureAuth(container);

			//Register all your dependencies
			container.Register(new TodoRepository());

            // clear 
            this.Plugins.RemoveAll(x => x is CsvFormat);
            //SetConfig(new EndpointHostConfig
            //{
            //    EnableFeatures = Feature.All.Remove(Feature.Csv),
            //});

            // install custom CSV
            Plugins.Add(new CsvFilenameFormat());
            //Plugins.Add(new CsvFormat());
            
            // not working
            //this.ContentTypeFilters.Register(ContentType.Csv, null, CsvSerializer.DeserializeFromStream);
            //this.ResponseFilters.Add((req, res, dto) =>
            //{
            //    if (req.ResponseContentType == ContentType.Csv)
            //    {
            //        res.AddHeader(HttpHeaders.ContentDisposition,
            //            string.Format("attachment;filename={0}.csv", "bogus"));
            //    }
            //});


		}

		/* Uncomment to enable ServiceStack Authentication and CustomUserSession
		private void ConfigureAuth(Funq.Container container)
		{
			var appSettings = new AppSettings();

			//Default route: /auth/{provider}
			Plugins.Add(new AuthFeature(() => new CustomUserSession(),
				new IAuthProvider[] {
					new CredentialsAuthProvider(appSettings), 
					new FacebookAuthProvider(appSettings), 
					new TwitterAuthProvider(appSettings), 
					new BasicAuthProvider(appSettings), 
				})); 

			//Default route: /register
			Plugins.Add(new RegistrationFeature()); 

			//Requires ConnectionString configured in Web.Config
			var connectionString = ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString;
			container.Register<IDbConnectionFactory>(c =>
				new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider));

			container.Register<IUserAuthRepository>(c =>
				new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));

			var authRepo = (OrmLiteAuthRepository)container.Resolve<IUserAuthRepository>();
			authRepo.CreateMissingTables();
		}
		*/

		public static void Start()
		{
			new AppHost().Init();
		}
	}
}
