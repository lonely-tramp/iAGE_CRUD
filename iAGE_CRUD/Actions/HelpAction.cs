using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iAGE_CRUD.Actions
{
    class HelpAction : Action
    {
        public HelpAction(IEnumerable<string> args) : base(args)
        {
        }

        public override void Execute()
        {
            Console.WriteLine("Доступны следующие команды -add -update -delete -get -getall -help");

            const string Id = "Id:<int>(больше нуля)";
            const string FirstName = "FirstName:<string>(не пустая строка)";
            const string LastName = "LastName:<string>(не пустая строка)";
            const string Salary = "Salary:<decimal>(больше нуля)";
            Console.WriteLine();
            Console.WriteLine("-add добавляет сотрудника");
            Console.WriteLine($"\tАргументы - {FirstName}, {LastName}, {Salary}");
            Console.WriteLine();
            Console.WriteLine("-update обновляет запись о сотруднике");
            Console.WriteLine($"\tАргументы - {Id}, {FirstName} или {LastName} или {Salary}");
            Console.WriteLine();
            Console.WriteLine("-delete удаляет запись о сотруднике");
            Console.WriteLine($"\tАргументы - {Id}");
            Console.WriteLine();
            Console.WriteLine("-get получить данные о сотруднике");
            Console.WriteLine($"\tАргументы - {Id}");
            Console.WriteLine();
            Console.WriteLine("-getall получить данные о всех сотрудниках");
            Console.WriteLine("\tБез аргументов");
            Console.WriteLine();
            Console.WriteLine("-help - вызов справки");

        }
    }
}
