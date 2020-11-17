using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RegularExpressions
{
    class Concordance
    {
        static public bool FindWord(string line, string word)
        {
            Regex r = new Regex(@"\b" + word + @"\b", RegexOptions.IgnoreCase);
            if (r.IsMatch(line))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public string FindLenghtWords(string line, int lenght)
        {
            Regex r = new Regex(@"\b" + @"\w{" + @lenght.ToString() + @"}" + @"\b", RegexOptions.IgnoreCase);
            string answer = "";
            Match words = r.Match(line);
            while (words.Success)
            {
                answer += words + " ";
                words = words.NextMatch();
            }
            return answer;
        }

        static public string DeleteOneSymbolWords(string line)
        {
            Regex r = new Regex(@"\b\w{1}\b", RegexOptions.IgnoreCase);
            string result = r.Replace(line, "");
            return result;
        }

        static public string ReplaceEnglishWords(string line)
        {
            Regex r = new Regex(@"\b\w*[a-z]\b", RegexOptions.IgnoreCase);
            string result = r.Replace(line, "...");
            return result;
        }

        static public string TelephoneNumbers(string line)
        {
            Regex[] r = new Regex[3];
            string result = "";
            r[0] = new Regex("[0-9]{2}-[0-9]{2}-[0-9]{2}");
            r[1] = new Regex("[0-9]{3}-[0-9]{3}");
            r[2] = new Regex("[0-9]{3}-[0-9]{2}-[0-9]{2}");
            for (int i = 0; i < 3; i++) 
            {
                Match numbers = r[i].Match(line);
                while (numbers.Success)
                {
                    result += numbers + " ";
                    numbers = numbers.NextMatch();
                }
            }
            return result;
        }

        public static List<string> ReadLinesFromFile(string path)
        {
            List<string> result;
            result = File.ReadLines(path).ToList();
            return result;
        }

        public static List<string> SplitToWords(string line)
        {
            List<string> words = new List<string>();
            Regex rx = new Regex(@"[а-яА-Яa-zA-Z]+",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(line);
            foreach (Match match in matches)
            {
                words.Add(match.Value.ToLower());
            }

            return words;
        }

        public static Dictionary<string, HashSet<int>> CreateDictinary(List<string> lines)
        {
            Dictionary<string, HashSet<int>> concordance = new Dictionary<string, HashSet<int>>();
            Dictionary<string, HashSet<int>> buffer = new Dictionary<string, HashSet<int>>();
            int page = 0;
            int index = 0;
            foreach (string line in lines)
            {
                if (index % 60 == 0)
                {
                    page++;
                    foreach (var value in buffer)
                    {
                        if (!concordance.ContainsKey(value.Key))
                        {
                            concordance[value.Key] = new HashSet<int>();
                        }

                        concordance[value.Key].Add(page);
                    }

                    buffer.Clear();
                }

                index++;
                foreach (string word in SplitToWords(line))
                {
                    if (!buffer.ContainsKey(word))
                    {
                        buffer[word] = new HashSet<int>();
                    }

                    buffer[word].Add(page);
                }
            }

            foreach (var value in buffer)
            {
                if (!concordance.ContainsKey(value.Key))
                {
                    concordance[value.Key] = new HashSet<int>();
                }

                concordance[value.Key].Add(page);
            }

            return concordance;
        }

        public static string CreateConcordance(Dictionary<string, HashSet<int>> concordance)
        {
            StringBuilder sb = new StringBuilder();
            string buffer = "";
            List<string> keys = concordance.Keys.OrderBy(o => o).ToList();
            char lastChar = '0';
            foreach (string key in keys)
            {
                if (key[0] != lastChar)
                {
                    lastChar = key[0];
                    sb.AppendFormat("\n{0}\n", lastChar.ToString().ToUpper());
                    buffer += lastChar.ToString().ToUpper();
                }
                sb.AppendFormat(
                    "{0}{1}:{2}\n",
                    key.PadRight(30 - concordance[key].Count.ToString().Length, '.'),
                    concordance[key].Count,
                    String.Join(", ", concordance[key])
                );
            }

            return sb.ToString();
        }
    }
}
