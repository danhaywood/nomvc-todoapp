using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Domain
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(string name) : base(name) { }
        public ToDoDbContext() { }

        //Add DbSet properties for root objects, thus:
        public DbSet<ToDoItem> ToDoItems{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Initialisation
            //Use the Naked Objects > DbInitialiser template to add an initialiser, then reference thus:
            Database.SetInitializer(new ToDoDbInitialiser());

            //Mappings
            //Use the Naked Objects > DbMapping template to create mapping classes & reference them thus:
            modelBuilder.Configurations.Add(new ToDoItemMapping());
        }
    }
}