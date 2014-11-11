using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GenTagsList
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@".\output.txt");
            for (int i=0;i<lines.Length;i++)
            {
                try
                {
                    //Console.WriteLine(line.Substring(1,line.Length-2));
                    string temp = lines[i].Substring(1, lines[i].Length - 2);
                    temp = temp.Substring(temp.IndexOf("tags=")+5);
                    int a = temp.IndexOf("&");
                    if (a >= 0)
                    {
                        temp = temp.Remove(a);
                    }
                    Console.WriteLine("{0} >> {1}", lines[i], temp);
                    lines[i] = temp;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(lines[i] + " " + ex.Message);
                    continue;
                }
            }
            List<string> list = new List<string>(lines);
            list = list.Distinct().ToList();
            list.Sort();
            using (StreamWriter outfile = new StreamWriter(@".\erza-auto.bat"))
            {
                foreach (string tag in list)
                {
                    outfile.WriteLine("erza.exe --clearcache {0}", tag.Replace("%", "%%"));
                    outfile.WriteLine("erza.exe");
                }
            }
            Console.WriteLine("Обработано тегов: {0} Уникальных: {1}", lines.Length, list.Count);
            Console.ReadKey();
        }
    }
}
