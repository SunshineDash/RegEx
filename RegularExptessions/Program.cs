using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace RegularExpressions
{
    class Program
    {
        static public void Menu(string path)
        {
            bool flag = true;
            string line = "";
            string word = "";
            string length = "";
            while (flag)
            {
                Console.WriteLine("Choose task");
                Console.WriteLine("1. Find word");
                Console.WriteLine("2. FindLenghtWords");
                Console.WriteLine("3. DeleteOneSymbolWords");
                Console.WriteLine("4. ReplaceEnglishWords");
                Console.WriteLine("5. TelephoneNumbers");
                Console.WriteLine("6. Create concordance");
                Console.WriteLine("7. Exit");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Write sentence:");
                        line = Console.ReadLine();
                        Console.WriteLine("Write word");
                        word = Console.ReadLine();
                        if (Concordance.FindWord(line, word)) Console.WriteLine("Сообщение содержит заданное слово");
                        else Console.WriteLine("Сообщение не содержит заданное слово");
                        break;
                    case "2":
                        Console.WriteLine("Write sentence:");
                        line = Console.ReadLine();
                        Console.WriteLine("Write length");
                        length = Console.ReadLine();
                        Console.WriteLine(Concordance.FindLenghtWords(line, Int32.Parse(length)));
                        break;
                    case "3":
                        Console.WriteLine("Write sentence:");
                        line = Console.ReadLine();
                        Console.WriteLine(Concordance.DeleteOneSymbolWords(line));
                        break;
                    case "4":
                        Console.WriteLine("Write sentence:");
                        line = Console.ReadLine();
                        Console.WriteLine(Concordance.ReplaceEnglishWords(line));
                        break;
                    case "5":
                        Console.WriteLine("Write sentence:");
                        line = Console.ReadLine();
                        Console.WriteLine(Concordance.TelephoneNumbers(line));
                        break;
                    case "6":
                        List<string> lines = Concordance.ReadLinesFromFile(path);
                        var dictionary = Concordance.CreateDictinary(lines);
                        string concordance = Concordance.CreateConcordance(dictionary);
                        File.WriteAllText("/Users/Asus/source/repos/RegularExptessions/RegularExptessions/result.txt", concordance);
                        Console.WriteLine("Concordance is ready!");
                        break;
                    case "7":
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Incorrect input. Please try again");
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            string path = "/Users/Asus/source/repos/RegularExptessions/RegularExptessions/War.txt";
            //string textPath = "/Users/Asus/source/repos/RegularExptessions/RegularExptessions/Text.txt";
            Menu(path);
        }
    }
}
