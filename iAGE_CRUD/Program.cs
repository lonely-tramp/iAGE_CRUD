using System.Linq;
using iAGE_CRUD.Actions;
using iAGE_CRUD.Actions.Employee.Actions;
using iAGE_CRUD.Constants;

namespace iAGE_CRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            var argumentsOfAction = args.Where((_, i) => i != 0);
            IAction actionStrategy;

            switch (args.FirstOrDefault())
            {
                case ActionStrategy.ADD:
                    actionStrategy = new AddEmployeeAction(argumentsOfAction);
                    break;
                
                case ActionStrategy.GET:
                    actionStrategy = new GetEmployeeAction(argumentsOfAction);
                    break;
                
                case ActionStrategy.GETALL:
                    actionStrategy = new GetAllEmployeeAction(argumentsOfAction);
                    break;
                
                case ActionStrategy.DELETE:
                    actionStrategy = new DeleteEmployeeAction(argumentsOfAction);
                    break;

                case ActionStrategy.UPDATE:
                    actionStrategy = new UpdateEmployeeAction(argumentsOfAction);
                    break;

                default:
                    actionStrategy = new HelpAction(null);
                    break;
            }

            actionStrategy.Execute();
        }
    }
}
