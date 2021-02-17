using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jewelry.ViewModels.Interfaces
{
    public interface IPermissions
    {
        void PermissionsAvailableForUser(User _currentUser);
    }
}
