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
            string uri = "";
            HttpWebResponse res;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://www.imdb.com/title/tt" + rnd.Next(1000000, 10000000)); //create new request
            try
            {
                res = (HttpWebResponse)req.GetResponse(); //trying to get response
                uri = res.ResponseUri.AbsoluteUri;
            }
            catch (System.Net.WebException)
            {
                req = null;
                getlink(new Random()); //high chance that will System.Net.WebException, trying again
            }
            Console.WriteLine(uri);
        }
    }
}
