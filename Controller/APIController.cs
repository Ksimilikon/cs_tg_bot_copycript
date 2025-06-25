using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            //httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
            //httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer fd63aa16-933d-4cbd-bed4-df4dcf536eff");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "ory_at_qj7vNrkj45jsR8Rdb_sbiWeKBHW1YyOW8Bre9165izI.vRx9WJXwCj4B3Fk_KZWt9NpD8Xon89eAqAvqHi5wcAQ");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
        public async Task<string> getMigrates(int count=10)
        {
            string query = @"
                query MigratedTokensLast24Hours {
                  Solana {
                    DEXTradeByTokens(
                      where: {
                        Block: {
                          Time: { since: ""2025-04-22T17:43:07Z"" }
                        },
                        Trade: {
                          Dex: {
                            ProtocolFamily: { is: ""Raydium"" }
                          },
                          Currency: {
                            MintAddress: { like: ""%pump%"" }
                          }
                        },
                        Transaction: {
                          Result: { Success: true }
                        }
                      },
                      limit: { count: " + count + @" },
                      orderBy: { ascending: Block_Time },
                      limitBy: { by: Trade_Currency_MintAddress, count: 1 }
                    ) {
                      Trade {
                        Currency {
                          Name
                          Symbol
                          MintAddress
                        }
                        Dex {
                          ProtocolName
                          ProtocolFamily
                        }
                        Market {
                          MarketAddress
                        }
                      }
                      Block {
                        Time
                      }
                    }
                  }
                }
            ";
            var payload = JsonConvert.SerializeObject(new { query = query });
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(httpClient.BaseAddress, content);

            return await response.Content.ReadAsStringAsync();
        }

        public void Dispose()
        {
            httpClient.Dispose();
            logger.Logger.Log(nameClass, "call Dispose()");
        }
    }
}
