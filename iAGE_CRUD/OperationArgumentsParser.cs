using System;
using System.Collections.Generic;
using System.Linq;

namespace iAGE_CRUD
{
    public class OperationArgumentsParser
    {
        public char Separator { get; }

        public OperationArgumentsParser(char separator)
        {
            Separator = separator;
        }

        private List<Argument> Arguments { get; set; } = new List<Argument>();

        public virtual bool TryParse(string[] args, out List<Argument> la)
        {
            la = null;
            var result = TrySplitArguments(args);
            if (result)
            {
                CleanDuplicatesArguments();
                la = Arguments;
            }
            return result;
        }

        private bool TrySplitArguments(string[] args)
        {
            const int maxCountToSplit = 2;
            char[] separators = { Separator };
            var results = new List<bool>();
            foreach (var a in args)
            {
                var splittedArg = a.Split(separators, maxCountToSplit);
                if (splittedArg.Length == maxCountToSplit)
                {
                    results.Add(true);
                    Arguments.Add(new Argument(splittedArg[0], splittedArg[1]));
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

        private void CleanDuplicatesArguments()
        {
            var result = new List<Argument>();
            var allKeys = Arguments
                .GroupBy(a => a.Key)
                .Select(g => new
                {
                    Name = g.Key,
                    Count = g.Select(a => a.Key).Count()
                });

            foreach (var k in allKeys)
            {
                var first = Arguments.First(a => a.Key == k.Name);
                if (k.Count > 1)
                {
                    Console.WriteLine($"Дублирование аргументов {k.Name} является неоднозначным.");
                    Console.WriteLine($"Будет выбран первый аргумен {first.Key}:{first.Value}");
                }
                result.Add(first);
            }

            Arguments = result;
        }
    }
}
