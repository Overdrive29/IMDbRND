using System;
using System.Net;

namespace IMDbRND
{
    class Program
    {
        static void Main(string[] args)
        {
            getlink(new Random());
            Console.WriteLine("anykey to close");
            Console.ReadLine();
        }
        static void getlink(Random rnd)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://www.imdb.com/title/tt" + rnd.Next(1000000, 10000000)); //create new request
            try
            {
                HttpWebResponse res = (HttpWebResponse)req.GetResponse(); //trying to get response
            }
            catch
            {
                getlink(new Random()); //high chance that will return 404 status code, trying again
            }
            finally
            {
                Console.WriteLine(req.RequestUri); //showing URI
            }
        }
    }
}
