using PCLStorage;
using Service.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.OfflineServices
{
    public class LocalStorage : ILocalStorage
    {
        public Task<IFile> CreateFile(string filename)
        {
            var isFileCreated = PCLHelper.CreateFile(filename, null);
            return isFileCreated;
        }

        public Task<bool> DeleteFile(string fileName)
        {
            var isFileDeleted = PCLHelper.DeleteFile(fileName, null);
            return isFileDeleted;
        }

        public Task<bool> IsFileExistAsync(string fileName)
        {
            var isFileExist = PCLHelper.IsFileExistAsync(fileName, null);
            return isFileExist;
        }

        public Task<bool> WriteTextAllAsync(string filename, string content = "")
        {
            var isFileSaved = PCLHelper.WriteTextAllAsync(filename, content);
            return isFileSaved;
        }
    }
}
