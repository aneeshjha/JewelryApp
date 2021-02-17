using Models;
using Service.ServiceInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Service.OfflineServices
{
    public class GetUserService : IGetUserService
    {
        public async Task<RepositoryResult<List<User>>> GetUSerDetailsAsync()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(GetUserService)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("Service.OfflineUsersData.json");
            List<User> userdetails = new List<User>();
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                if (!string.IsNullOrWhiteSpace(json))
                {
                    var rootobject = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(json);
                    userdetails = rootobject;
                }
            }
            return RepositoryResult<List<User>>.Create(userdetails, string.Empty, System.Net.HttpStatusCode.OK);
        }
    }
}
