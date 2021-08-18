using System;
using System.Linq;
using iAge_CRUD.Actions;
using iAge_CRUD.Constants;

namespace iAge_CRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Доступны следующие команнды -add -update -delete -get -getall");
                return;
            }
            var action = args[0];
            var argumentsOfAction = args.Where((_, i) => i != 0)
                .ToArray();

            IAction actionStrategy;

            switch (action)
            {
                case ActionStrategy.ADD:
                    actionStrategy = new AddAction(argumentsOfAction);
                    break;
                
                case ActionStrategy.GET:
                    actionStrategy = new GetAction(argumentsOfAction);
                    break;
                
                case ActionStrategy.GETALL:
                    actionStrategy = new GetAllAction(argumentsOfAction);
                    break;
                
                case ActionStrategy.DELETE:
                    actionStrategy = new DeleteAction(argumentsOfAction);
                    break;
                
                case ActionStrategy.UPDATE:
                    actionStrategy = new UpdateAction(argumentsOfAction);
                    break;

                default:
                    actionStrategy = null;
                    Console.WriteLine("Доступны следующие команнды -add -update -delete -get -getall");
                    break;
            }

            actionStrategy?.Execute();
        }
    }
}
