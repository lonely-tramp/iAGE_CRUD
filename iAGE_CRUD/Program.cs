using System.Linq;
using iAGE_CRUD.Actions;
using iAGE_CRUD.Actions.Employee.Actions;

namespace iAGE_CRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            IAction action;

            switch (args.FirstOrDefault())
            {
                case Constants.Actions.Add:
                    action = new AddEmployeeAction(args);
                    break;
                
                case Constants.Actions.Get:
                    action = new GetEmployeeAction(args);
                    break;
                
                case Constants.Actions.Getall:
                    action = new GetAllEmployeeAction(args);
                    break;
                
                case Constants.Actions.Delete:
                    action = new DeleteEmployeeAction(args);
                    break;

                case Constants.Actions.Update:
                    action = new UpdateEmployeeAction(args);
                    break;

                default:
                    action = new HelpAction(null);
                    break;
            }

            action.Execute();
        }
    }
}
