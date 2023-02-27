using Contracts;
using Entities;
using Entities.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ShippingRepository : RepositoryBase<Shipping>, IShippingRepository
    {
        public ShippingRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {

        }

        public void CreateShipping(Shipping shipping)
        {
            Create(shipping);
        }

        public void DeleteShipping(Shipping shipping)
        {
            Delete(shipping);
        }

        public async Task<ICollection<Shipping>> GetAllShipping(bool trackChanges)
        {
            return await FindAll(e => e.ShippingId > 0, trackChanges).OrderBy(shi => shi.ShippingId).ToListAsync();
        }

        public async Task<Shipping> GetShippingById(int id, bool trackChanges)
        {
            return await FindByCondition(shi => shi.ShippingId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }

        public void PutShipping(Shipping shipping, bool trackChanges)
        {
            Update(shipping);
        }
    }
}
