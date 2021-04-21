using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace myproject
{
    public class LoadingData
    {
        private int textCodeFormat;

        public static Boolean IsValidUri(String uri)
        {
            return Uri.IsWellFormedUriString(uri, UriKind.Absolute);//uri validation
        }
       
        public static async Task<List<Post>> LoadD(int numberN)
        {
            string url = "https://hacker-news.firebaseio.com/v0/beststories.json?print=pretty";
           
            using (HttpResponseMessage response = await Helper.HnewsClient.GetAsync(url))//make the call wait for the response
            {
                if(response.IsSuccessStatusCode)
                {
                    var fields = await response.Content.ReadAsAsync<List<int>>();
                    List<Post> posts = new List<Post>();
                    
                    foreach (var field in fields.Take(numberN))
                    {
                        string itemurl = $"https://hacker-news.firebaseio.com/v0/item/{field}.json?print=pretty";

                        bool ur = IsValidUri(itemurl);
                        if (ur == false)
                        {
                            Console.WriteLine("invalid url");
                            Console.ReadKey();
                        }
                      

                        using (HttpResponseMessage itemresponse = await Helper.HnewsClient.GetAsync(itemurl))//make the call wait for the response
                        {

                            if (itemresponse.IsSuccessStatusCode)
                            {
                                var post = await itemresponse.Content.ReadAsAsync<Post>();

                                if (String.IsNullOrEmpty(post.by))//author and title validation
                                {
                                    post.error = "the field: author is null or empty ";
                                    
                                }else if (String.IsNullOrEmpty(post.title))
                                {
                                    post.error = "the field: title is null or empty " ;
                                    
                                }else if ((post.title.Length > 256))
                                {
                                    post.error = "the field: title cannot be longer than 256 ";

                                }


                                posts.Add(post);//adding post into a list
                                
                            }
                        }
                    }
                    Console.WriteLine(JsonConvert.SerializeObject(posts,Formatting.Indented));

                    return posts;
                    
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }

    }
}     
   
        
    
          

           
    
 

