using System.Data.Entity;

namespace Domain
{
    public class ToDoDbInitialiser : DropCreateDatabaseIfModelChanges<ToDoDbContext>
    {

        protected override void Seed(ToDoDbContext toDoDbContext)
        {
            //var f1 = NewFoo("Foo 1", context);
        }

        //private Foo NewFoo(string name, MyDbContext context)
        //{
        //    Foo foo = new Foo() {Description = name};
        //    context.Foos.Add(foo);
        //    return foo;
        //}
    }
}