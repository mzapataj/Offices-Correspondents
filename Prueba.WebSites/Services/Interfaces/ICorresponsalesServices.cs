using Common.Models;
using Prueba.Model;
using Prueba.Model.Dao;

namespace WebSites.Services.Interfaces
{
    public interface ICorresponsalesServices
    {
        public string Path { get; }
        public Uri Uri { get; set; }
        Task<IEnumerable<Corresponsal>> GetAll();

        Task<Corresponsal> Details(long? id);

        Task<ResponseMessage<Corresponsal>> Create(Corresponsal newCorresponsal);

        Task<ResponseMessage<Corresponsal>> Edit(long? id, Corresponsal editCorresponsal);

        Task<ResponseMessage<Corresponsal>> Delete(long? id);
        Task<IEnumerable<CorresponsalesOficinas>> GetCorresponsalesCountOficinas();
    }
}
