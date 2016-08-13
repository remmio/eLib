using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using eLib.Exceptions;
using eLib.Security;
using Newtonsoft.Json;
using static eLib.Exceptions.Operation;

namespace eLib.Utils
{
    public static class HttpHelper
    {
        public static Uri BaseUri { get; set; }
        public static string Token { get; set; }


        public static async Task<T> HttpGet<T>(string path)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseUri;

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);

                var response = await client.GetAsync(path);
                if (!response.IsSuccessStatusCode)
                    throw new ApiException(response.StatusCode, await response.Content.ReadAsStringAsync(), response.ReasonPhrase);
                return await response.Content.ReadAsAsync<T>();
            }
        }

        public static async Task HttpGet(string path)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseUri;

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);

                var response = await client.GetAsync(path);
                if (!response.IsSuccessStatusCode)
                    throw new ApiException(response.StatusCode, await response.Content.ReadAsStringAsync(), response.ReasonPhrase);
            }
        }

        public static async Task<T> HttpAdd<T>(object obj, string path)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseUri;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);

                var jObject = JsonConvert.SerializeObject(obj, Formatting.None,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        DateFormatHandling = DateFormatHandling.IsoDateFormat,
                        PreserveReferencesHandling = PreserveReferencesHandling.None,
                        NullValueHandling = NullValueHandling.Ignore
                    });

                var response = await client.PostAsync(path, new StringContent(jObject, Encoding.UTF8, "application/json"));
                if (!response.IsSuccessStatusCode)
                    throw new ApiException(response.StatusCode, await response.Content.ReadAsStringAsync(), response.ReasonPhrase);
                return await response.Content.ReadAsAsync<T>();
            }
        }

        public static async Task<Operation> HttpAdd<T>(T obj, string path)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseUri;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);

                var jObject = JsonConvert.SerializeObject(obj, Formatting.None,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        DateFormatHandling = DateFormatHandling.IsoDateFormat,
                        PreserveReferencesHandling = PreserveReferencesHandling.None,
                        NullValueHandling = NullValueHandling.Ignore
                    });

                var response = await client.PostAsync(path, new StringContent(jObject, Encoding.UTF8, "application/json"));
                if (!response.IsSuccessStatusCode)
                    throw new ApiException(response.StatusCode, await response.Content.ReadAsStringAsync(), response.ReasonPhrase);
                return Succes(response.Headers.Location?.ToString());
            }
        }

        public static async Task<Operation> HttpUpdate<T>(T obj, string objectPath)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseUri;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);

                var jObject = JsonConvert.SerializeObject(obj, Formatting.None,
                     new JsonSerializerSettings
                     {
                         ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                         DateFormatHandling = DateFormatHandling.IsoDateFormat,
                         PreserveReferencesHandling = PreserveReferencesHandling.None,
                         NullValueHandling = NullValueHandling.Ignore
                     });

                var response = await client.PutAsync(objectPath, new StringContent(jObject, Encoding.UTF8, "application/json"));
                if (!response.IsSuccessStatusCode)
                    throw new ApiException(response.StatusCode, await response.Content.ReadAsStringAsync(), response.ReasonPhrase);
                return Succes(response.Headers.Location?.ToString());
            }
        }

        public static async Task<Operation> HttpDelete(string objectPath)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseUri;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);

                var response = await client.DeleteAsync(objectPath);
                if (!response.IsSuccessStatusCode)
                    throw new ApiException(response.StatusCode, await response.Content.ReadAsStringAsync(), response.ReasonPhrase);
                return Succes(response.ReasonPhrase);
            }
        }

        public static async Task<Operation> HttpLogin(string path, string username, string password, bool rememberMe = default(bool))
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseUri;
                var responseMessage = await client.PostAsync(path, new FormUrlEncodedContent(
                        new[]
                        {
                            new KeyValuePair<string, string>("grant_type", "password"),
                            new KeyValuePair<string, string>("username", username),
                            new KeyValuePair<string, string>("password", password)
                        }
                    ));

                var tokenModel = await responseMessage.Content.ReadAsAsync<TokenModel>();

                if (!string.IsNullOrEmpty(tokenModel?.AccessToken))
                {
                    Token = tokenModel.AccessToken;

                    if (rememberMe)
                        WebCredentials.Set(tokenModel.AccessToken);

                    return Succes(tokenModel.AccessToken);
                }

                Token = string.Empty;
                return Failed(responseMessage.ReasonPhrase);
            }
        }

        public static async Task<Operation> HttpLogOut(string path)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseUri;

                var response = await client.PostAsJsonAsync("api/Account/Logout", new { });
                await response.Content.ReadAsStringAsync();
                Token = string.Empty;
                if (!response.IsSuccessStatusCode)
                    throw new ApiException(response.StatusCode, await response.Content.ReadAsStringAsync(), response.ReasonPhrase);
                return Succes(response.ReasonPhrase);
            }
        }
    }

    public class TokenModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(".expires")]
        public string ExpiresAt { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(".issued")]
        public string IssuedAt { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("userName")]
        public string Username { get; set; }
    }
}
