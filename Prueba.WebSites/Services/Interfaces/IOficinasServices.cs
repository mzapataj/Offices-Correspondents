using Common.Models;
using Prueba.Model;
using Prueba.Model.Dao;


namespace WebSites.Services.Interfaces
{
    public interface IOficinasServices
    {
        public string Path { get; }
        Task<IEnumerable<Oficina>> GetAll();

        Task<Oficina> Details(long? id);

        Task<ResponseMessage<Oficina>> Create(OficinaCreate newCorresponsal);

        Task<ResponseMessage<Oficina>> Edit(long? id, OficinaCreate editCorresponsal);

        Task<ResponseMessage<Oficina>> Delete(long? id);

        Task<ResponseMessage<ICollection<Oficina>>> GetOficinasCorresponsal(long? id);
    }
}
