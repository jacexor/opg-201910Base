using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using opg_201910_interview.Core.Services.Clients;
using opg_201910_interview.Core.Services.Files;
using opg_201910_interview.InputModels;
using opg_201910_interview.Models;

namespace opg_201910_interview.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        private readonly IFileService _fileService;

        public ClientController(IClientService clientService, IFileService fileService)
        {
            _clientService = clientService;
            _fileService = fileService;
        }

        [HttpPost("")]
        public async Task<ActionResult<List<ClientModel>>> GetClientsAsync([FromBody] GetClientsInputModel clientInput)
        {
            var dateFormat = await _fileService.GetFilesDateFormatBaseByMajorityAsync(clientInput.FileDirectoryPath);
            var files = await _fileService.ReadFilesAsync(clientInput.FileDirectoryPath, dateFormat);
            var clients = await _clientService.ClientFileEnumeratorAsync(files, clientInput.ClientId);

            var result = new List<ClientModel>();

            foreach(var client in clients)
            {
                result.Add(new ClientModel()
                {
                    Name = client.Name,
                    Date = client.Date
                });
            }

            return Ok(result);
        }
    }
}
