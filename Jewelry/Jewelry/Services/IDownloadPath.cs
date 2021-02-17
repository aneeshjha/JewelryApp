using System;
using System.Collections.Generic;
using System.Text;

namespace Jewelry.Services
{
    public interface IDownloadPath
    {
        System.Threading.Tasks.Task<bool> SaveFileAsyn(string content);
    }

}
