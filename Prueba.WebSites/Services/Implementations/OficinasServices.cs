using System.Text.Json;
using WebSites.Extensions;
using Microsoft.Extensions.Configuration;
using WebSites.Services.Interfaces;
using Common.Models;
using Prueba.Model;
using Prueba.Model.Dao;

namespace WebSites.Services.Implementations
{
    public class OficinasServices : IOficinasServices
    {


        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        private Uri _uri;


        public string Path { get => $"{_uri.AbsoluteUri}Oficinas"; }


        public OficinasServices(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;

            _uri = new Uri(_config.GetSection("WebApiUrl").Get<string>());
        }


        public async Task<IEnumerable<Oficina>> GetAll() 
        {
            return await _httpClient.SendGetDefaultRequest<IEnumerable<Oficina>>($"{Path}/Index");
        }


        public async Task<Oficina> Details(long? id)
        {
            return await _httpClient.SendGetDefaultRequest<Oficina>($"{Path}/Details/{id}");
        }


        public async Task<ResponseMessage<Oficina>> Create(OficinaCreate newCorresponsal)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"{Path}/Create"
            );

            var content = JsonContent.Create(newCorresponsal);

            var result = await _httpClient.SendRequest<JsonContent, ResponseMessage<Oficina>>(content, request);

            return result;
        }


        public async Task<ResponseMessage<Oficina>> Edit(long? id, OficinaCreate editCorresponsal)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"{Path}/Edit/{id}"
            );

            var content = JsonContent.Create(editCorresponsal);

            var result = await _httpClient.SendRequest<JsonContent, ResponseMessage<Oficina>>(content, request);

            return result;
        }

        public async Task<ResponseMessage<Oficina>> Delete(long? id)
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

            return await response.ReadAndDeserialize<ResponseMessage<Oficina>>();
        }


        public async Task<ResponseMessage<ICollection<Oficina>>> GetOficinasCorresponsal(long? id)
        {
            return await _httpClient.SendGetDefaultRequest<ResponseMessage<ICollection<Oficina>>>($"{Path}/GetOficinasCorresponsal/{id}");
        }
    }
}
