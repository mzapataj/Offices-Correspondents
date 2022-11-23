using System.Text.Json;
using WebSites.Extensions;
using Microsoft.Extensions.Configuration;
using WebSites.Services.Interfaces;
using Common.Models;
using Prueba.Model;
using Prueba.Model.Dao;

namespace WebSites.Services.Implementations
{
    public class CorresponsalesServices : ICorresponsalesServices
    {


        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        private Uri _uri;
        public Uri Uri { get => _uri; set => _uri = value; }

        public string Path { get => $"{_uri.AbsoluteUri}Corresponsales"; }


        public CorresponsalesServices(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;

            _uri = new Uri(_config.GetSection("WebApiUrl").Get<string>());
        }


        public async Task<IEnumerable<Corresponsal>> GetAll() 
        {
            return await _httpClient.SendGetDefaultRequest<IEnumerable<Corresponsal>>($"{Path}/Index");
        }


        public async Task<Corresponsal> Details(long? id)
        {
            return await _httpClient.SendGetDefaultRequest<Corresponsal>($"{Path}/Details/{id}");
        }


        public async Task<ResponseMessage<Corresponsal>> Create(Corresponsal newCorresponsal)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"{Path}/Create"
            );

            var content = JsonContent.Create<Corresponsal>(newCorresponsal);

            var result = await _httpClient.SendRequest<JsonContent, ResponseMessage<Corresponsal>>(content, request);

            return result;
        }


        public async Task<ResponseMessage<Corresponsal>> Edit(long? id, Corresponsal editCorresponsal)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"{Path}/Edit/{id}"
            );

            var content = JsonContent.Create<Corresponsal>(editCorresponsal);

            var result = await _httpClient.SendRequest<JsonContent, ResponseMessage<Corresponsal>>(content, request);

            return result;
        }

        public async Task<ResponseMessage<Corresponsal>> Delete(long? id)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"{Path}/Delete/{id}"
            );

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
               response.EnsureSuccessStatusCode();                
            }

            return await response.ReadAndDeserialize<ResponseMessage<Corresponsal>>();
        }

        public async Task<IEnumerable<CorresponsalesOficinas>> GetCorresponsalesCountOficinas()
        {
            return await _httpClient.SendGetDefaultRequest<IEnumerable<CorresponsalesOficinas>>($"{Path}/GetCorresponsalesCountOficinas");
        }
    }
}
