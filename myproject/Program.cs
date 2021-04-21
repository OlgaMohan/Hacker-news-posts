using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json;

namespace myproject
{


    class Program
    {
        //hackernews --posts n
        //--posts how many posts to print.A positive integer <= 100.
        //title and author are non empty strings not longer than 256 characters.
        //2. uri is a valid URI
        //3. points, comments and rank are integers >= 0.
        //4. Your solution is tested;
        //5. That there is robust input checking

        

        static void Main(string[] args) { CommandLineApplication.Execute<Program>(args);
            
        }
        [Option("-n|--n", Description = "writea a number")]//property
        private int NumberN { get; }

        private void OnExecute()
        {
           
            if (NumberN > 100 || NumberN<1) {
                Console.WriteLine("Valid range is between 1 and 100!");
                Console.ReadKey();
                return;
            }
            Console.WriteLine(NumberN);
            Helper.InitializeClient();//if the number is wrong don't create a http client
            var Info = LoadingData.LoadD(NumberN);
            Console.ReadKey();
        }





    }
}
    






