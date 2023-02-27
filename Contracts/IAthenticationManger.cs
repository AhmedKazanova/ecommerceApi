using Entities.DataTranferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAuthenticationManager
    {
        Task<string> ValidateUser(UserLoginDto userForAuth);
        Task<string> CreateToken();
    }
}
