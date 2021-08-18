using System;
using System.Collections.Generic;
using System.Linq;
using iAge_CRUD.Model;

namespace iAge_CRUD.Parsers
{
    public class ArgumentsParser
    {
        public char Separator { get; }

        public ArgumentsParser(char separator)
        {
            Separator = separator;
        }

        private List<Argument> _arguments;

        public bool TryParse(string[] args, out Dictionary<string, string> argumnets)
        {
            argumnets = null;
            if (!TrySplitArguments(args)) return false;
            argumnets = CleanDuplicatesArguments();
            return true;
        }

        private bool TrySplitArguments(string[] args)
        {
            _arguments = new List<Argument>();
            const int maxCountToSplit = 2;
            char[] separators = { Separator };
            var results = new List<bool>();
            foreach (var a in args)
            {
                var splittedArg = a.Split(separators, maxCountToSplit);
                if (splittedArg.Length == maxCountToSplit)
                {
                    results.Add(true);
                    _arguments.Add(new Argument(splittedArg[0], splittedArg[1]));
                }
                else
                {
                    Console.WriteLine($"Неверный формат аргумента {a}");
                    Console.WriteLine("Допустимый формат - Key:Value");
                    results.Add(false);
                }
            }

            return results.All(b => b);
        }

        private Dictionary<string, string> CleanDuplicatesArguments()
        {
            var arguments = new List<Argument>();
            var allKeys = _arguments
                .GroupBy(a => a.Key)
                .Select(g => new
                {
                    Name = g.Key,
                    Count = g.Select(a => a.Key).Count()
                });

            foreach (var k in allKeys)
            {
                var first = _arguments.First(a => a.Key == k.Name);
                if (k.Count > 1)
                {
                    Console.WriteLine($"Дублирование аргументов {k.Name} является неоднозначным.");
                    Console.WriteLine($"Будет выбран первый аргумен {first.Key}:{first.Value}");
                }
                arguments.Add(first);
            }

            var uniqueArguments = new Dictionary<string, string>();
            arguments.ForEach(a => uniqueArguments.Add(a.Key, a.Value));


            return uniqueArguments;
        }

    }
}
