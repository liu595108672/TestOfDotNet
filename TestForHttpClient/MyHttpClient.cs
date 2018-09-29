using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace TestForHttpClient
{
    public class MyHttpClient
    {
        public bool TestOne()
        {
            var client = new HttpClient();
            string url = @"http://baidu.com";
            var result = client.GetStringAsync(url).Result;

            Console.WriteLine(result);

            return true;
        }

        public bool TestTwo()
        {
            var client = new HttpClient();
            string url = @"http://baidu.com";
            var result = client.GetAsync(url).Result;

            if (result.IsSuccessStatusCode)
            {
                var header = result.Headers;
                var reasonPhrase = result.ReasonPhrase;
                var statuscode = result.StatusCode;
                var content = result.Content;
                var vesion = result.Version;
            }
            return true;
        }


        public bool TestThree()
        {
            string url = @"http://localhost:5000/api/values?api-version=2.0";
            Person p = new Person() { Name = "Jay", Age = 23, Gender = true };
            Person p2 = new Person() { Name = "Zhaoyang", Age = 23, Gender = true };
            var jsonP = JsonConvert.SerializeObject(p2);

            HttpClient httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");


            var tmpResult = httpClient.PostAsJsonAsync<Person>(url, p).Result;


          


          

            return true;
        }

        public class Person
        {
            public string Name { set; get; }
            public int Age { set; get; }
            public bool Gender { set; get; }

        }
    }
}
