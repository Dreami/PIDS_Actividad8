using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileOutputs;
using System.IO;
using System.Collections;

namespace Actividad8
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo d = new DirectoryInfo(Outputs.getAllFiles());
            FileInfo[] Files = d.GetFiles("*.txt");

            string output_path8 = @"C:\Users\maple\Documents\9° Semester\CS13309_Archivos_HTML\a8_matricula.txt";
            string output;

            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<string> sortedWords = new List<string>();

            int count = 0;
            Hashtable postings = new Hashtable();
            
            foreach (FileInfo file in Files)
            {

                output = "";
                var watchEach = System.Diagnostics.Stopwatch.StartNew();
                string htmlContent = File.ReadAllText(file.FullName);
                htmlContent.Trim();

                string[] eachWord = htmlContent.Split(' ');
                try
                {
                    foreach (string word in eachWord)
                    {
                        if (!string.IsNullOrEmpty(word))
                        {
                            if (!word.Equals(" "))
                            {
                                word.Replace(",", "")
                                    .Replace(".", "");
                            }

                            if (postings.ContainsKey(word))
                            {
                                Posting existingWord = (Posting) postings[word];
                                existingWord.addWord(word);
                                postings[word] = existingWord;
                            }
                            else
                            {
                                postings.Add(word, new Posting(word, file.Name, count));
                            }
                        }
                    }
                }
                catch (ArgumentNullException argExc)
                {
                    Console.WriteLine(argExc.StackTrace);
                }
                catch (KeyNotFoundException keyNotFoundExc)
                {
                    Console.WriteLine(keyNotFoundExc.StackTrace);
                }

                List<String> tokenList = postings.Keys.Cast<String>().ToList();

                foreach (String token in tokenList)
                {
                    if (Array.IndexOf(eachWord, token) >= 0)
                    {
                        Posting foundWord = (Posting) postings[token];
                        output += "\n" + foundWord.printScore() + "\n";
                    }
                    else
                    {
                        postings.Remove(token);
                    }
                }

                output += "\n" + file.Name + " finished in " + watchEach.Elapsed.TotalMilliseconds.ToString() + " ms";
                Console.WriteLine(output);
                watchEach.Stop();
                Outputs.output_print(output_path8, output);
                count++;
            }

            output = "\nAll files sorted in\t" + watch.Elapsed.TotalMilliseconds.ToString() + " ms";
            Console.WriteLine(output);
            watch.Stop();
            Outputs.output_print(output_path8, output);

            Console.Read();
        }
    }
}
