using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad8
{
    class Posting
    {
        private string word;
        private int position;
        private int repetitions;
        private HashSet<string> repeatsPerFile = new HashSet<string>();

        public Posting(string w, string fileName, int startPosition)
        {
            word = w;
            repetitions = 1;
            repeatsPerFile.Add(fileName);
            position = startPosition;
        }

        public string getWord()
        {
            return word;
        }

        public void addWord(string fileName)
        {
            repetitions++;
            repeatsPerFile.Add(fileName);
        }

        public override bool Equals(object rc)
        {
            return Equals(rc as Posting);
        }

        public string printScore()
        {
            return word + ";" + repetitions + ";" + repeatsPerFile.Count + "; Posting: " + position;
        }
    }
}
