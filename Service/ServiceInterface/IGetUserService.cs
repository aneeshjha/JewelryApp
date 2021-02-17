using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterface
{
    public interface IGetUserService
    {
        Task<RepositoryResult<List<User>>> GetUSerDetailsAsync();
    }
}
