using PCLStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterface
{
    public interface ILocalStorage
    {
        Task<bool> IsFileExistAsync(string fileName);
        Task<IFile> CreateFile(string filename);
        Task<bool> WriteTextAllAsync(string filename, string content = "");
        Task<bool> DeleteFile(string fileName);
    }
}
