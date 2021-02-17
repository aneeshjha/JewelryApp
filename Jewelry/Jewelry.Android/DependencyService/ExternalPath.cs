using System;
using System.IO;
using System.Linq;
using Jewelry.Droid.DependencyService;
using Jewelry.Services;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

[assembly: Xamarin.Forms.Dependency(typeof(ExternalPath))]
namespace Jewelry.Droid.DependencyService
{
    public class ExternalPath : IDownloadPath
    {
        public async System.Threading.Tasks.Task<bool> SaveFileAsyn(string content)
        {
            try
            {
                PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (!(status==PermissionStatus.Granted))
                {
                    return false;
                }
                var allFiles = await Android.OS.Environment.ExternalStorageDirectory.ListFilesAsync();
                if (allFiles == null)
                {
                    return false;
                }
                Java.IO.File newDbFolder = allFiles?.FirstOrDefault(f => f.Name.Contains("Download", StringComparison.InvariantCultureIgnoreCase));
                if (!string.IsNullOrEmpty(content))
                {
                    var filename = "Jewelry.txt";
                    var location = Path.Combine(newDbFolder.AbsolutePath, filename);
                    File.Delete(location);
                    File.WriteAllText(location, content);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
        }
    }
}