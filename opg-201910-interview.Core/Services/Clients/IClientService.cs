using opg_201910_interview.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace opg_201910_interview.Core.Services.Clients
{
    public interface IClientService
    {
        Task<string> GetClientPathAsync(int clientId);

        Task<List<File>> ClientFileEnumeratorAsync(List<File> files, int clientId);
    }
}
