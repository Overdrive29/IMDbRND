using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Database
{
    public class Program
    {
        
        public static async Task Main(string[] args)
        {
            Downloader dwn = Downloader.GetInstance();
            using (IMDBContext context = new IMDBContext())
            {
                dwn.DatabaseReady += () =>
                {
                    var currentCount = context.Titles.Count();
                    Console.WriteLine(currentCount);
                    foreach (var Title in TitleOperations.StartParsing(currentCount))
                    {
                        context.Add(Title);
                        Console.WriteLine("Добавлена запись ->" + Title.Tconst);
                    }
                    context.SaveChanges();
                    Console.WriteLine("База обновлена!");
                };



                
                
                while (true)
                {
                    await dwn.GetArchiveAsync();
                    Thread.Sleep(TimeSpan.FromDays(1));
                }



            }
        }
        
    }
}