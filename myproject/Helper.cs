using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Linq;

namespace myproject
{
    public class Helper
    {
        public static HttpClient HnewsClient { get; set; }

        public static void InitializeClient()
        {
            HnewsClient = new HttpClient();
            HnewsClient.BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/");//beststories
            HnewsClient.DefaultRequestHeaders.Accept.Clear();
            HnewsClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
