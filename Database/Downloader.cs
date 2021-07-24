using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Database
{
    public class Downloader
    {
        const string DOWNLOADURI = "https://datasets.imdbws.com/title.basics.tsv.gz";
        const string ARCHIVEDATA = "data.gz";
        const string DATA = "data.txt";
        public event Action DatabaseReady;
        public static DateTime CreationDate;
        static Downloader instance;

        private Downloader() { }
        public static Downloader GetInstance()
        {
            if (instance == null)
                instance = new Downloader();
            return instance;
        }

        public async Task GetArchiveAsync()
        {
            await Task.Run(() => getArchive());
        }
        private void getArchive()
        {
                WebClient client = new WebClient();
                client.DownloadFileAsync(new Uri(DOWNLOADURI), ARCHIVEDATA);
                client.DownloadProgressChanged += (_, info) => Console.WriteLine(info.ProgressPercentage);
                client.DownloadFileCompleted += (_, e) => UnZip();
                CreationDate = DateTime.Now;
                Console.WriteLine($"Создание файла завершено в {DateTime.Now}\nСледующая загрузка в {CreationDate + TimeSpan.FromDays(1)}");
        }

        async Task UnZipAsync()
        {
            await Task.Run(() => UnZip());
        }
        void UnZip()
        {
            using (FileStream archive = new FileStream(ARCHIVEDATA, FileMode.Open))
            {
                using (FileStream data = new FileStream(CreationDate.Date.ToShortDateString() + DATA, FileMode.Create))
                {
                    using (GZipStream zipStream = new GZipStream(archive, CompressionMode.Decompress))
                    {
                        zipStream.CopyTo(data);
                        Console.WriteLine("data.txt создан");
                    }
                }
            }
            new FileInfo(ARCHIVEDATA).Delete();
            DatabaseReady.Invoke();
            Console.WriteLine("Событие!");
        }

        
        
    }
}
