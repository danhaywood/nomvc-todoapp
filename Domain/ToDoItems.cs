using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NakedObjects;


namespace Domain
{
    [IconName("ToDoItem.png")]
    public class ToDoItems
    {

        #region Injected Services
        // This region should contain properties to hold references to any services required by the
        // object.  Use the 'injs' shortcut to add a new service.
        public IDomainObjectContainer Container { set; protected get; }
        #endregion

        #region NewToDo
        [MemberOrder("1")]
        public ToDoItem NewToDo(

            [RegEx(Validation = "\\w[@&:\\-\\,\\.\\+ \\w]*")] [MaxLength(50)] string description,
            Category category,
            [Optionally] DateTime dueBy,
            [Optionally] decimal cost)
        {
            var obj = Container.NewTransientInstance<ToDoItem>();
            obj.Description = description;
            obj.Category = category;
            obj.DueBy = dueBy;
            obj.Cost = cost;
            Container.Persist(ref obj);
            return obj;
        }
        #endregion

        #region NotYetComplete
        [QueryOnly]
        [MemberOrder("2")]
        public IQueryable<ToDoItem> NotYetComplete()
        {
            return Container.Instances<ToDoItem>().Where(x => !x.Complete);
        }
        #endregion

        #region Complete
        [QueryOnly]
        [MemberOrder("3")]
        public IQueryable<ToDoItem> Complete()
        {
            return Container.Instances<ToDoItem>().Where(x => x.Complete);
        }
        #endregion

        [NakedObjectsIgnore]
        public string ValidateDueBy(DateTime dueBy)
        {
            return IsMoreThanOneWeekInPast(dueBy) ? "Due by date cannot be more than one week old" : null;
        }

        private static bool IsMoreThanOneWeekInPast(DateTime dateTime)
        {
            // TODO
            return false;
        }
    }
}
