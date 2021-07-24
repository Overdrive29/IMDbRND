using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Database
{
    public class TitleOperations
    {

        
        static TitleOperations instance;

        private TitleOperations() {}
        public static TitleOperations GetInstance()
        {
            if (instance == null)
                instance = new TitleOperations();
            return instance;
        }

        public static IEnumerable<Title> StartParsing(int currentCount)
        {
            using (StreamReader sr = new StreamReader(Downloader.CreationDate.ToShortDateString() + "Data.txt"))
            {
                for (int i = 0; i < currentCount; i++)
                {
                    sr.ReadLine();
                }
                while (!sr.EndOfStream)
                {
                    yield return CreateTitle(sr.ReadLine());
                }
            }
        }

        public static Title CreateTitle(params string[] args)
        {
            Title result = new Title();
            result.Tconst = args[0];
            result.TitleType = args[1];
            result.PrimaryTitle = args[2];
            result.OriginalTitle = args[3];
            result.IsAdult = IsAdultChecker(args[4]);

            if (int.TryParse(args[5], out _))
                result.StartYear = int.Parse(args[5]);
            if (int.TryParse(args[6], out _))
                result.EndYear = int.Parse(args[6]);
            if (int.TryParse(args[7], out _))
                result.RuntimeMinutes = int.Parse(args[7]);
            result.Genres = args[8];

            return result;
        }
        public static Title CreateTitle(string line)
        {
            string[] args = line.Parse();
            return CreateTitle(args);
        }
        static bool IsAdultChecker(string arg)
        {
            int intArg;
            if (int.TryParse(arg, out intArg))
                return intArg > 0;
            return false;
        }

        public static string TitleToString(Title title)
        {
            string result = "";
            var props = title.GetType();
            foreach (var prop in props.GetProperties())
            {
                if (prop.Name == "Id")
                    continue;
                if (prop.Name == "IsAdult")
                {
                    if (title.IsAdult)
                        result += 1 + "\t";
                    else
                        result += 0 + "\t";
                    continue;
                }
                if(prop.GetValue(title) == null)
                {
                    result += "\\N\t";
                    continue;
                }
                result += prop.GetValue(title) + "\t";
            }
            return result.Trim();
        }
        
    }
}
