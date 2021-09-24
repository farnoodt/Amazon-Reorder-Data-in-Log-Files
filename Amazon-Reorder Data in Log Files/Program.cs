using System;
using System.Collections.Generic;
using System.Linq;

namespace Amazon_Reorder_Data_in_Log_Files
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] s = new string[] { "a1 9 2 3 1", "g1 act car", "zo4 4 7", "ab1 off key dog", "a8 act zoo", "a2 act car"};// { "a1 9 2 3 1", "g1 act car", "zo4 4 7", "ab1 off key dog", "a8 act zoo" };//{ "dig1 8 1 5 1", "let1 art can", "dig2 3 6", "let2 own kit dig", "let3 art zero" };
            //s = s.Substring(0, s.IndexOf(' '));
            ReorderLogFiles(s);
            Console.WriteLine("Hello World!");
        }

       

        static string[] ReorderLogFiles(string[] logs)
        {
            List<Record> LD = new List<Record>();
            List<Record> LL = new List<Record>();
            List<string> Res = new List<string>();

            for (int i = 0; i < logs.Length; i++)
            {
                string s = logs[i];

                if (s[s.IndexOf(" ") + 1] < 'a')
                    LD.Add(new Record(s.Substring(0, s.IndexOf(' ') + 1), s.Substring(s.IndexOf(' ') + 1)));
                else
                    LL.Add(new Record(s.Substring(0, s.IndexOf(' ') + 1), s.Substring(s.IndexOf(' ') + 1)));
            }
            LL.Sort();
            List<Record> SL = LL.OrderBy(x => x.logs).ThenBy(x => x.Id).ToList();

            while (SL.Count != 0 || LD.Count != 0)
            {
                if (SL.Count != 0)
                {
                    Res.Add(SL[0].Id + SL[0].logs);
                    SL.RemoveAt(0);
                }
                else
                {
                    Res.Add(LD[0].Id + LD[0].logs);
                    LD.RemoveAt(0);
                }
            }
            Console.WriteLine(string.Join(" , ", Res));
            return Res.ToArray();
        }
    }

    public class Record : IComparable<Record>
    {
        public string Id;
        public string logs;

        public Record(string Id, string logs)
        {
            this.Id = Id;
            this.logs = logs;
        }

        public int CompareTo(Record other)
        {
            if (other == null)
                return 1;
            return Comparer<string>.Default.Compare(logs, other.logs);
        }
    }

    
}
