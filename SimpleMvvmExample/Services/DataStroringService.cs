using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SimpleMvvmExample.Services
{
    public class DataStoringService
    {
        private const string filepath = "userinformation.json";

        public List<Models.UserInformation> LoadAll()
        {
            if (File.Exists(filepath))
            {
                var stream = File.OpenText(filepath);
                var file_string = stream.ReadToEnd();
                stream.Close();

                return JsonConvert.DeserializeObject<List<Models.UserInformation>>(file_string);
            }

            else return new List<Models.UserInformation>();
        }

        public void StoreUserinformation(Models.UserInformation userinformation)
        {
            var users = LoadAll();
            users.Add(userinformation);
            var json_string = JsonConvert.SerializeObject(users);
            var bytearray = System.Text.Encoding.ASCII.GetBytes(json_string);

            var stream = File.CreateText(filepath);
            stream.Write(json_string);
            stream.Close();

        }
    }
}
