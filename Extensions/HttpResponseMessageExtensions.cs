namespace Extensions
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using System;
    using Newtonsoft.Json;

    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> DeserializeResponse<T>(this HttpResponseMessage response)
        {
            var strResponse = await response.Content.ReadAsStringAsync();
            return Task.Factory.StartNew(()=>JsonConvert.DeserializeObject<T>(strResponse.Replace("Welcome to .net core",""))).Result;
        }
    }
}