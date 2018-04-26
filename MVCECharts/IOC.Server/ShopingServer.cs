using System.Linq;
using Repository_基础结构层.Models;
using Repository_基础结构层;
namespace IOC.Server
{
    public class ShopingServer : IShopingServer
    {
        private IRepositoryBase<ShopingInfo> _shopingRepository;
        public ShopingServer(IRepositoryBase<ShopingInfo> shopingRepository)
        {
            _shopingRepository = shopingRepository;
        }

        public IQueryable<ShopingInfo> GetAll()
        {
          return _shopingRepository.FindAll();
      
        }

        public string Placeholderfill(string htmlContext)
        {
            var entity = _shopingRepository.FindAll().FirstOrDefault();
            htmlContext = htmlContext.Replace("{{SHOPINGNAME}}", entity.ShopingName);
            htmlContext = htmlContext.Replace("{{SHOPINGPric}}", entity.ShopingPric.ToString());
            htmlContext = htmlContext.Replace("{{SHOPINGNuber}}", entity.ShopingCount.ToString());
            return htmlContext;
        }

    }
}
