using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IShippingRepository
    {
        Task<ICollection<Shipping>> GetAllShipping(bool trackChanges);
        Task<Shipping> GetShippingById(int id, bool trackChanges);
        void CreateShipping(Shipping shipping);
        void DeleteShipping(Shipping shipping);
        void PutShipping(Shipping shipping, bool trackChanges);
    }
}
