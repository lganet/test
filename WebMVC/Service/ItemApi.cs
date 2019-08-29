using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebMVC.Service
{
    public class ItemApi
    {

        private HttpClient client()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44301/");
            
            return client;
        }

        public async Task<List<ItemViewModel>> Get(string item)
        {
            var result = new List<ItemViewModel>();
            var apiResult = await client().GetAsync($"v1/item/{item}");

            if (apiResult.IsSuccessStatusCode)
            {
                var results = apiResult.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<List<ItemViewModel>>(results);

                return result;
            }

            if (apiResult.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            var errorResult = apiResult.Content.ReadAsStringAsync().Result;
            var error = JsonConvert.DeserializeAnonymousType(errorResult, new { ErrorCode = 500, Message = "Internal Error" } );

            throw new ApplicationException(error.Message);
        }


        public async Task Add(ItemViewModel item)
        {
            var apiResult = await client().PostAsJsonAsync<ItemViewModel>($"v1/item", item);

            if (!apiResult.IsSuccessStatusCode)
            {
                var errorResult = apiResult.Content.ReadAsStringAsync().Result;
                var error = JsonConvert.DeserializeAnonymousType(errorResult, new { ErrorCode = 500, Message = "Internal Error" });

                throw new ApplicationException(error.Message);
            }

        }

    }
}
