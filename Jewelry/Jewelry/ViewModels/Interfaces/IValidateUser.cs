using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jewelry.ViewModels.Interfaces
{
    public interface IValidateUser
    {
        //Validate and get User details.
        Task<User> ValidateUser(string userName, string password);
    }
}
