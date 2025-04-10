using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace cs_tg_bot.Controller
{
    class APIController : Controller, IDisposable
    {
        protected override string nameClass => "APIController";
        private HttpClient httpClient;
        public APIController(string baseUrl)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseUrl);
        }
        ~APIController()
        {
            httpClient.Dispose();
            logger.Logger.Log(nameClass, "call destructor");
        }
        public async Task<string> getResponseAsync(string curlApi)
        {
            try
            {
                HttpResponseMessage responseMessage = await httpClient.GetAsync(curlApi);
                responseMessage.EnsureSuccessStatusCode();
                await logger.Logger.LogAsync(nameClass+"::getResponseAsync()",  "get response");
                return await responseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                logger.Logger.Log(nameClass+"::getResponseAsync()", "error response::"+e.Message);
            }
            return "";
        }

        public void Dispose()
        {
            httpClient.Dispose();
            logger.Logger.Log(nameClass, "call Dispose()");
        }
    }
}
