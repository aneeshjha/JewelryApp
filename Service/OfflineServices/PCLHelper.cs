using PCLStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.OfflineServices
{
    public static class PCLHelper
    {
        /// <summary>
        /// Check if file exists
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="rootFolder"></param>
        /// <returns></returns>
        public async static Task<bool> IsFileExistAsync(this string fileName, IFolder rootFolder = null)
        {
            // get hold of the file system  
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
            ExistenceCheckResult folderexist = await folder.CheckExistsAsync(fileName);
            if (folderexist == ExistenceCheckResult.FileExists)
            {
                return true;

            }
            return false;
        }

        /// <summary>
        /// Write to file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="content"></param>
        /// <param name="rootFolder"></param>
        /// <returns></returns>
        public async static Task<bool> WriteTextAllAsync(string filename, string content = "", IFolder rootFolder = null)
        {
            IFile file = await filename.CreateFile(rootFolder);
            await file.WriteAllTextAsync(content);
            return true;
        }

        /// <summary>
        /// Create File
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="rootFolder"></param>
        /// <returns></returns>
        public async static Task<IFile> CreateFile(this string filename, IFolder rootFolder = null)
        {
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
            IFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            return file;
        }


        /// <summary>
        /// Delete if any exisisting file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="rootFolder"></param>
        /// <returns></returns>
        public async static Task<bool> DeleteFile(this string fileName, IFolder rootFolder = null)
        {
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
            bool exist = await fileName.IsFileExistAsync(folder);
            if (exist == true)
            {
                IFile file = await folder.GetFileAsync(fileName);
                await file.DeleteAsync();
                return true;
            }
            return false;
        }
    }
}
