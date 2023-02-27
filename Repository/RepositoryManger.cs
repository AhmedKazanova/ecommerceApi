using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManger : IRepositoryManger
    {

        private ApplicationDbContext _repositoryContext;
        private IShippingRepository _IShippingRepository;


        public RepositoryManger(ApplicationDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IShippingRepository Shipping
        {
            get
            {
                if (_IShippingRepository == null)
                    _IShippingRepository = new ShippingRepository(_repositoryContext);
                return _IShippingRepository;
            }
        }

        public Task Save()
        {
            return _repositoryContext.SaveChangesAsync();
        }
    }
}
