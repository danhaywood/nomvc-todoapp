// Copyright © Naked Objects Group Ltd ( http://www.nakedobjects.net). 
// All Rights Reserved. This code released under the terms of the 
// Microsoft Public License (MS-PL) ( http://opensource.org/licenses/ms-pl.html) 

using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RestfulObjects.Mvc;
using Simple.App_Start;
using NakedObjects.Web.Mvc;
using WebActivator;

[assembly: PreApplicationStartMethod(typeof (NakedObjectsStart), "PreStart")]
[assembly: PostApplicationStartMethod(typeof (NakedObjectsStart), "PostStart")]

namespace Simple.App_Start {
    public static class NakedObjectsStart {
        public static void PreStart() {
            InitialiseLogging();
            RegisterRoutes(RouteTable.Routes);            
        }

        public static void PostStart() {
            RegisterBundles(BundleTable.Bundles);

            RunWeb.Run();
            DependencyResolver.SetResolver(new NakedObjectsDependencyResolver());
            RestConfig.RestPostStart();

            // Without this any value type fields with a default value will be set to mandatory by the MS unobtrusive validation
            // - that overrides the required NOF behaviour based on the 'Optionally' attribute.
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;

            CorsConfig.RegisterCors(GlobalConfiguration.Configuration);
        }

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{*favicon}", new {favicon = @"(.*/)?favicon.ico(/.*)?"});
            routes.IgnoreRoute("{*nakedobjects}", new {nakedobjects = @"(.*/)?nakedobjects.ico(/.*)?"});
      
            RestConfig.RestRegisterRoutes(routes); // must be rest first 
            RunMvc.RegisterGenericRoutes(routes);

            // for development
            RestfulObjectsControllerBase.AcceptHeaderStrict = false;
        }

        // this may be moved to BundleConfig as required - here just to simplify Nuget install  
        public static void RegisterBundles(BundleCollection bundles) {
            // register Naked Objects bundles  

            bundles.Add(new ScriptBundle("~/bundles/nakedobjectsajax").Include(
                "~/Scripts/jquery.address-{version}.js",
                "~/Scripts/jquery.json-{version}.js",
                "~/Scripts/jstorage*",
                "~/Scripts/NakedObjects-Ajax*"));

            bundles.Add(new ScriptBundle("~/bundles/nakedobjectsbasic").Include(
                "~/Scripts/NakedObjects-Basic*"));

            bundles.Add(new ScriptBundle("~/bundles/jquerydatepicker").Include(
                "~/Scripts/ui/i18n/jquery.ui.datepicker-en-GB*"));

            bundles.Add(new StyleBundle("~/Content/nakedobjectscss").Include(
                "~/Content/NakedObjects.css"));
        }

        public static void InitialiseLogging() {
            // uncomment and add appropriate Common.Logging package
            // http://netcommon.sourceforge.net/docs/2.1.0/reference/html/index.html

            //var properties = new NameValueCollection();
        
            //properties["configType"] = "INLINE";
            //properties["configFile"] = @"C:\Naked Objects\nologfile.txt";

            //LogManager.Adapter = new Common.Logging.Log4Net.Log4NetLoggerFactoryAdapter(properties);

        }
    }
}