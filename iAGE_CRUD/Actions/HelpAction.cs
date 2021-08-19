using System;
using System.Collections.Generic;

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

            const string id = "Id:<int>(больше нуля)";
            const string firstName = "FirstName:<string>(не пустая строка)";
            const string lastName = "LastName:<string>(не пустая строка)";
            const string salary = "Salary:<decimal>(больше нуля)";
            Console.WriteLine();
            Console.WriteLine("-add добавляет сотрудника");
            Console.WriteLine($"\tАргументы - {firstName}, {lastName}, {salary}");
            Console.WriteLine();
            Console.WriteLine("-update обновляет запись о сотруднике");
            Console.WriteLine($"\tАргументы - {id}, {firstName} или {lastName} или {salary}");
            Console.WriteLine();
            Console.WriteLine("-delete удаляет запись о сотруднике");
            Console.WriteLine($"\tАргументы - {id}");
            Console.WriteLine();
            Console.WriteLine("-get получить данные о сотруднике");
            Console.WriteLine($"\tАргументы - {id}");
            Console.WriteLine();
            Console.WriteLine("-getall получить данные о всех сотрудниках");
            Console.WriteLine("\tБез аргументов");
            Console.WriteLine();
            Console.WriteLine("-help - вызов справки");

        }
    }
}
