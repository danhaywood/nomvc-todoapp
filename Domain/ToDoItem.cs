using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NakedObjects;

namespace Domain {

    [IconName("ToDoItem.png")]
    public class ToDoItem {

        #region Injected Services
        public ToDoItems ToDoItems { set; protected get; }
        public IDomainObjectContainer Container { set; protected get; }
        #endregion

        #region Title, Icon, Id
        [Key, Hidden]
        public virtual int Id { get; set; }

        public string Title() {
            var buf = new TitleBuilder();
            buf.Append(Description);
            if(Complete) {
                buf.Append("- Completed!");
            } else {
                if(DueBy.HasValue) {
                    buf.Append(" due by", DueBy.Value.ToShortDateString());
                }
            }

            return buf.ToString();
        }
      
        public string IconName() {
            return "ToDoItem-" + (!Complete ? "todo" : "done");
        }
        #endregion

        #region Description (property)
        [RegEx(Validation = "\\w[@&:\\-\\,\\.\\+ \\w]*")]
        [TypicalLength(50)]
        [MemberOrder(Sequence = "1")]
        public virtual string Description { get; set; }
        #endregion

        #region Category (property)
        [MemberOrder(Sequence = "2")]
        public virtual Category Category { get; set; }
        public string DisableCategory()
        {
          return "Use action to update both category and subcategory";
        }

        [Idempotent]
        [MemberOrder(Sequence = "2")]
        public void UpdateCategory(Category category)
        {
            Category = category;
        }
        #endregion

        #region OwnedBy (hidden property, commented out for now...)
        //[Hidden]
        //public virtual string OwnedBy { get; set; }

        #endregion

        #region Complete (property), Completed (action), NotYetCompleted (action)
        [MemberOrder(Sequence = "3")]
        [Disabled]
        public virtual bool Complete { get; set; }

        [Idempotent]
        [PresentationHint("x-highlight")]
        [MemberOrder(Sequence = "1.1")]
        public void Completed( )
        {
            Complete = true;
        }
        public string DisableCompleted()
        {
            return Complete ? "Already completed" : null;
        }


        [Idempotent]
        [MemberOrder(Sequence = "1.2")]
        public void NotYetCompleted( )
        {
            Complete = false;
        }
        public string DisableNotYetCompleted() {
            return !Complete ? "Not yet completed" : null;
        }
        #endregion

        #region DueBy (property)
        [PresentationHint("x-key")]
        [Optionally]
        [Mask("d")]
        [MemberOrder(Sequence = "4")]
        public virtual DateTime? DueBy { get; set; }
        public string ValidateDueBy(DateTime? dueBy) {
            return dueBy.HasValue ? null : ToDoItems.ValidateDueBy(dueBy.Value);
        }

        #endregion

        #region Cost (property), UpdateCost (action)
        [MemberOrder(Sequence = "5")]
        public virtual decimal? Cost { get; set; }
        public string DisableCost() {
            return "Update using action";
        }

        [Idempotent]
        [MemberOrder(Sequence = "3")]
        public void UpdateCost(decimal cost)
        {
            Cost = cost;
        }
        public decimal Default0UpdateCost()
        {
            return Cost.GetValueOrDefault();
        }
        public string ValidateUpdateCost(decimal? cost)
        {
          return cost.GetValueOrDefault(0) < 0? "Cost must be positive": null;
        }
        #endregion

        #region Notes (property)
        [MultiLine(NumberOfLines = 5, Width = 40)]
        [Optionally]
        [MemberOrder(Sequence = "6")]
        public virtual string Notes { get; set; }
        #endregion

        #region Dependencies (collection), AddTo, RemoveFrom
        private ICollection<ToDoItem> _dependencies = new List<ToDoItem>();

        [MemberOrder(Sequence = "10")]
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        public virtual ICollection<ToDoItem> Dependencies {
            get {
                return _dependencies;
            }
            set {
                _dependencies = value;
            }
        }

        [MemberOrder("4.1")]
        public void AddDependency(ToDoItem value) {
            if(!(_dependencies.Contains(value))) {
                _dependencies.Add(value);
            }
        }
        public string DisableAddDependency()
        {
            return Choices0AddDependency().Count > 0 ? null : "No todo items available";
        }
        public IList<ToDoItem> Choices0AddDependency() {
            var all = Container.Instances<ToDoItem>().ToList();
            all.RemoveAll(all.Contains);
            all.Remove(this);
            return all;
        }



        [MemberOrder("4.2")]
        public void RemoveDependency(ToDoItem value) {
            if(_dependencies.Contains(value)) {
                _dependencies.Remove(value);
            }
        }
        public string DisableRemoveDependency() {
            return Choices0RemoveDependency().Count > 0 ? null : "No todo items to remove";
        }
        public IList<ToDoItem> Choices0RemoveDependency() {
            return _dependencies.ToList();
        }
        #endregion

    }

    public enum Category
    {
        Professional, Domestic, Other
    }
}

