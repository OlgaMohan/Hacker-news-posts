---


---

<h4 id="hacker-news-posts">Hacker news posts</h4>
<p><strong>Description</strong><br>
The project is created to print out n posts from Hacker News using the Hacker News free API.</p>
<h4 id="section"></h4>
<p><strong>Installation</strong><br>
It is a Console application I have made in Visual Studio, in .Net Core so it can be run on Linux and Mac too.<br>
Also the following NuGet packages must be installed:</p>
<ul>
<li>HeackerNewsReader 1.0.9</li>
<li>McMaster.Extensions.CommandLineUtils 4.0.0-beta.74</li>
<li>Microsoft.AspNet.WebApi.Client 5.2.7</li>
<li>Microsoft.NETCore.App 2.1.0</li>
<li>Newtonsoft.Json 13.0.1</li>
</ul>
<p>.</p>
<h2 id="section-1"></h2>
<p><strong>Usage</strong></p>
<ol>
<li>Fields/items we want to print out, provided by <a href="https://github.com/HackerNews/API?fbclid=IwAR3kUIRY7Rppl4sfwJtwM1Ze7rvjQ168kQLioJCfRce2lcKwpvlc10gW-dI#hacker-news-api">https://github.com/HackerNews/API?fbclid=IwAR3kUIRY7Rppl4sfwJtwM1Ze7rvjQ168kQLioJCfRce2lcKwpvlc10gW-dI#hacker-news-api</a>.</li>
</ol>
<p>public class Post<br>
{</p>
<pre><code>    public string title { get; set; }//Title
    public string url { get; set; }//uri
    public string by { get; set; }//author
    public int score { get; set; }//points
    public int descendants { get; set; }//    comments 
    public int rank { get; set; }
    
}
</code></pre>
<ol start="2">
<li>The below class named Helper is created to allow us to make calls on the internet. We have a static property to make sure we open the Http Client one per application.</li>
</ol>
<p>public class Helper<br>
{<br>
public static HttpClient HnewsClient { get; set; }</p>
<pre><code>    public static void InitializeClient()
    {
        HnewsClient = new HttpClient();
        HnewsClient.BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/");//beststories
        HnewsClient.DefaultRequestHeaders.Accept.Clear();
        HnewsClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
}
</code></pre>
<p>For MediaTypeWithQualityHeaderValue Class we use the : System.Net.Http.Headers.<br>
For HttpClient we use: using System.Net.Http.</p>
<ol start="3">
<li>LoadingData class is created making the actual call to the API. We open up a call, new request and wait for the response. At the end of the using statement it will close down everything we don’t leave ports open up. An URI validation it is also has been made, also the title and author field are validated too. In case these are empty or null or the string is larger than 256, we receive a message.</li>
</ol>
<p>public class LoadingData<br>
{<br>
private int textCodeFormat;</p>
<pre><code>    public static Boolean IsValidUri(String uri)
    {
        return Uri.IsWellFormedUriString(uri, UriKind.Absolute);
    }
   
    public static async Task&lt;List&lt;Post&gt;&gt; LoadD(int numberN)
    {
        string url = "https://hacker-news.firebaseio.com/v0/beststories.json?print=pretty";
       
        using (HttpResponseMessage response = await Helper.HnewsClient.GetAsync(url))//make the call wait for the response
        {
            if(response.IsSuccessStatusCode)
            {
                var fields = await response.Content.ReadAsAsync&lt;List&lt;int&gt;&gt;();
                List&lt;Post&gt; posts = new List&lt;Post&gt;();
                
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
                            var post = await itemresponse.Content.ReadAsAsync&lt;Post&gt;();

                            if (String.IsNullOrEmpty(post.by))
                            {
                                post.error = "the field: author is null or empty ";
                                
                            }else if (String.IsNullOrEmpty(post.title))
                            {
                                post.error = "the field: title is null or empty " ;
                                
                            }else if ((post.title.Length &gt; 256))
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
</code></pre>
<p>We have to add:</p>
<ul>
<li>using System.Net.Http</li>
<li>using System.Threading.Tasks;</li>
<li>using System.Collections.Generic;</li>
<li>using Newtonsoft.Json - because we have our output in Json</li>
<li>using System.Linq;</li>
</ul>
<ol start="4">
<li>In our Main it is used the property “Option”, this way we are able to add an argument from outside, how many posts to print out exactly. Also numberN it is validated, cannot be smaller then one or bigger than 100. In case it is out of range the Http Client is not created.</li>
</ol>
<p>static void Main(string[] args) { CommandLineApplication.Execute(args);</p>
<pre><code>    }
    [Option("-n|--n", Description = "writea a number")]//property
    private int NumberN { get; }

    private void OnExecute()
    {
       
        if (NumberN &gt; 100 || NumberN&lt;1) {
            Console.WriteLine("Valid range is between 1 and 100!");
            Console.ReadKey();
            return;
        }
        Console.WriteLine(NumberN);
        Helper.InitializeClient();//if the  number is wrong don't create a http client
        var Info = LoadingData.LoadD(NumberN);
        Console.ReadKey();
    }
</code></pre>
<p>The  McMaster.Extensions.CommandLineUtils library needs to be included.</p>

