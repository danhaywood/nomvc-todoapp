using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Domain;
using NakedObjects.Boot;
using NakedObjects.Core.Context;
using NakedObjects.Core.NakedObjectsSystem;
using NakedObjects.Core.Util;
using NakedObjects.EntityObjectStore;
using NakedObjects.Web.Mvc;
using NakedObjects.Web.Mvc.Helpers;

namespace Simple.App_Start {
    public class RunWeb : RunMvc {

		// Return null for no REST support
		// Return a non empty string to support REST at that URL segment
        public static string RestRoot {
            get { return "rest"; }
        }

        protected override NakedObjectsContext Context {
            get { return HttpContextContext.CreateInstance(); }
        }

        protected override IServicesInstaller MenuServices {
            get {
                return new ServicesInstaller(new ToDoItems());
            }
        }

        protected override IServicesInstaller ContributedActions {
            get { return new ServicesInstaller(); }
        }

        protected override IServicesInstaller SystemServices {
            get { return new ServicesInstaller(new SimpleEncryptDecrypt()); }
        }


        protected override IObjectPersistorInstaller Persistor
        {
            get
            {
                // Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0"); //For in-memory database
                // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MyDbContext>()); //Optional behaviour for CodeFirst
                var installer = new EntityPersistorInstaller();

                installer.UsingCodeFirstContext(() => new ToDoDbContext())
                         .AssociateTypes(() => new[] {typeof(ToDoItem)});

                return installer;
            }
        }

        public static void Run() {
		    Assert.AssertTrue("Rest root may not be empty", RestRoot == null || RestRoot.Trim().Length > 0);
            new RunWeb().Start();
        }
    }
}