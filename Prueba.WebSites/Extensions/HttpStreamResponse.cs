using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebSites.Extensions
{
    public static class HttpStreamResponse
    {
        public static async Task<T> SendRequestWithoutBody<T>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage,
                                                                    Func<object, bool> httpErrorHandler = null)
        {            
            using (httpRequestMessage)
            {
                var response = await httpClient.SendAsync(httpRequestMessage,
                                    HttpCompletionOption.ResponseHeadersRead);

                if (!response.IsSuccessStatusCode)
                {
                    if (httpErrorHandler(response)) {
                        return default(T);
                    }
                    else
                    {
                        response.EnsureSuccessStatusCode();                        
                    }                    
                }

                return await response.ReadAndDeserialize<T>();
            }
        }

        public static async Task<T> SendGetDefaultRequest<T>(this HttpClient httpClient, string url)
        {
            var request = new HttpRequestMessage(
                   HttpMethod.Get,
                   url
               );

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead);

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                response.EnsureSuccessStatusCode();
                return stream.ReadAndDeserializeFromJson<T>();
            }
        }
        public static async Task<string> SendGetDefaultRequestText(this HttpClient httpClient, string url)
        {
            var request = new HttpRequestMessage(
                   HttpMethod.Get,
                   url
               );

            var response = await httpClient.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead);

            return await response.Content.ReadAsStringAsync();
            
        }

        public static async Task<Response> SendPostDefaultJsonRequest<Request,Response>(this HttpClient httpClient, string url, Request body)
        {
            Response tempResponse;
            var memoryContentStream = new MemoryStream();

            memoryContentStream.SerializeToJsonAndWrite(body);

            memoryContentStream.Seek(0, SeekOrigin.Begin);

            using (var request = new HttpRequestMessage(
                    HttpMethod.Post,
                    url
                ))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var streamContent = new StreamContent(memoryContentStream))
                {
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    request.Content = streamContent;

                    var response = await httpClient.SendAsync(request,
                                            HttpCompletionOption.ResponseHeadersRead);
                    response.EnsureSuccessStatusCode();

                    var stream = await response.Content.ReadAsStreamAsync();
                    tempResponse = stream.ReadAndDeserializeFromJson<Response>();                    
                }
            }

            return tempResponse;
        }

        public static async Task<Response> SendRequest<Request, Response>(this HttpClient httpClient, HttpContent body, HttpRequestMessage httpRequestMessage,
                                                                                Func<object, bool> httpErrorHandler = null)
        {            
            using (httpRequestMessage)
            {
                httpRequestMessage.Content = body;

                var response = await httpClient.SendAsync(httpRequestMessage, HttpCompletionOption.ResponseHeadersRead);
                
                if (!response.IsSuccessStatusCode)
                {

                    if (httpErrorHandler is not null && httpErrorHandler(response))
                    {
                        return default(Response);
                    }
                    else
                    {
                        response.EnsureSuccessStatusCode();
                    }      
                }

                return await response.ReadAndDeserialize<Response>();
            }
        }


        public static async Task<T> ReadAndDeserialize<T>(this HttpResponseMessage response)
        {

            var contentTypeResponse = response.Content.Headers.ContentType.MediaType;

            if (contentTypeResponse == "application/json")
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    return stream.ReadAndDeserializeFromJson<T>();
                }
            }
            else
            {
                throw new NotSupportedException($"Not supported '{contentTypeResponse}' content media type on HTTP Response.");
            }
        }

    }
}
