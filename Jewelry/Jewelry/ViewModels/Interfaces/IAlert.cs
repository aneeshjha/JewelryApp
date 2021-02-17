using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jewelry.ViewModels.Interfaces
{
    interface IAlert
    {
        Task ShowAlert(string message, string okText, string cancelText);
    }
}
