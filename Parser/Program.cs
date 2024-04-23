// See https://aka.ms/new-console-template for more information
using Nito.AsyncEx;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Parser
{
    class Program
    {
        public static readonly List<Conductor> conductors = new List<Conductor>();
        
        public static void Main()
        {
            string path = @"C:\Users\OlehKozlovskyi\source\repos\Parser\Parser\file.txt";
            string text = ReadFile(path).Result;
            string[] data = SplitTextOnStatements(text);
            AddData(data);
            WriteLine(conductors);
            WriteInFile(true);
        }

        //Asynchronic read data from file
        public static async Task<string> ReadFile(string path)
        {
            string textFromFile = string.Empty;
            using (FileStream fstream = new FileStream(path, FileMode.Open))
            {
                byte[] buffer = new byte[fstream.Length];
                await fstream.ReadAsync(buffer, 0, buffer.Length);
                textFromFile = Encoding.Default.GetString(buffer);
            }
            return textFromFile;
        }

        public static void WriteInFile(bool other, string? connector = null)
        {
            using (StreamWriter writeText = new StreamWriter($"Connectors X{connector}.txt"))
            {
                foreach (var conductor in conductors)
                {
                    if(conductor.Connector == connector)
                    {
                        writeText.Write($"{conductor.From} {conductor.To}\n");
                    }

                    if (other && conductor.Connector!="1" && conductor.Connector != "2" && 
                        conductor.Connector != "3" && conductor.Connector != "4" && conductor.Connector != "5")
                    {
                        writeText.Write($"{conductor.From} {conductor.To}\n");
                    }
                }
            }
        }

        public static string[] SplitTextOnStatements(string text)
        {
            return text.Split("\n");
        }

        private static void AddData(string[] data)
        {
            foreach (string s in data)
            {
                List<string> str = s.Split(" ").ToList();
                str.RemoveAll(x => string.IsNullOrWhiteSpace(x));
                if(str.Count > 3)
                {
                    conductors.Add(new Conductor(from: str[0], to: $"{str[2] + "\\" + str[3]}", str[0], str[1]));
                }
                else
                {
                    conductors.Add(new Conductor(from: str[0], to: $"{str[1] + "\\" + str[2]}"));
                }
            }
        }

        public static void WriteLine(List<Conductor> conductors)
        {
            foreach (var conductor in conductors)
            {
                Console.WriteLine($"{conductor.From} {conductor.To}");
            }
        }
    }
}
