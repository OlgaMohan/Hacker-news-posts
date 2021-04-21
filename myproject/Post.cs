using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Linq;

namespace myproject
{
    public class Post
    {

        public string title { get; set; }//Title
        public string url { get; set; }//uri
        public string by { get; set; }//author
        public int score { get; set; }//points
        public int descendants { get; set; }// comments 
        public int rank { get; set; }
        public string error { get; set; }
        
    }
}
