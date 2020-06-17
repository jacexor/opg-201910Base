using opg_201910_interview.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace opg_201910_interview.Core.Services.Clients
{
    public class ClientService : IClientService
    {
        public async Task<List<File>> ClientFileEnumeratorAsync(List<File> files, int clientId)
        {
            var pFiles = files;
            return await Task.Run(() =>
            {
                // SIMULATE DB CALL
                var sort = new List<string>();
                switch (clientId)
                {
                    case 1001:
                        sort.Add("shovel");
                        sort.Add("waghor");
                        sort.Add("blaze");
                        sort.Add("discus");
                        break;
                    case 2001:
                        sort.Add("orca");
                        sort.Add("widget");
                        sort.Add("eclair");
                        sort.Add("talon");
                        break;
                }

                var files = new List<File>();

                foreach(var item in sort)
                {
                    var dFiles = pFiles.Where(p => p.Name == item).OrderBy(p=>p.Date).ToList();
                    files.AddRange(dFiles);
                }

                return files;
            });
        }

        public async Task<string> GetClientPathAsync(int clientId)
        {
            return await Task.Run(() =>
            {
                // SIMULATE DB CALL
                switch (clientId)
                {
                    case 1001:
                        return "UploadFiles/ClientA";
                    case 2001:
                        return "UploadFiles/ClientB";
                }

                return string.Empty;
            });
        }
    }
}
