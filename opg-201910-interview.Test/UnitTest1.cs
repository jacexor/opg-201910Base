using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using Moq;
using Newtonsoft.Json;
using opg_201910_interview.Controllers;
using opg_201910_interview.Core.Services.Clients;
using opg_201910_interview.Core.Services.Files;
using opg_201910_interview.InputModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace opg_201910_interview.Test
{
    public class UnitTest1
    {
        ClientController _clientController;

        IClientService _clientService;

        IFileService _fileService;

        HttpClient _client;

        IWebHostBuilder _webHostBuilder;

        ITestOutputHelper _output;

        public UnitTest1(ITestOutputHelper output)
        {
            var mockEnvironment = new Mock<IHostingEnvironment>();

            _output = output;
            _clientService = new ClientService();
            _fileService = new FileService(mockEnvironment.Object);
            _clientController = new ClientController(_clientService, _fileService);

            _webHostBuilder = new WebHostBuilder()
                    .UseContentRoot(CalculateRelativeContentRootPath())
                    .UseEnvironment("Development")
                    .UseConfiguration(new ConfigurationBuilder()
                        .SetBasePath(CalculateRelativeContentRootPath())
                        .AddJsonFile("appsettings.json")
                        .Build())
                    .UseStartup<opg_201910_interview.Startup>();
            
            var testServer = new TestServer(_webHostBuilder);

            _client = testServer.CreateClient();
        }

        [Fact]
        public async Task Valid_GetClientsAsync()
        {
            var formData = new Dictionary<string, object>
                  {
                    {"ClientId", int.Parse(_webHostBuilder.GetSetting("ClientSettings:ClientId"))},
                    {"FileDirectoryPath", _webHostBuilder.GetSetting("ClientSettings:FileDirectoryPath")}
                  };

            var response = await _client.PostAsync("api/clients", 
                new StringContent(JsonConvert.SerializeObject(formData), System.Text.Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            _output.WriteLine(responseString);
        }


        string CalculateRelativeContentRootPath() =>
            Path.Combine(PlatformServices.Default.Application.ApplicationBasePath,
            @"..\..\..\..\opg-201910Base-master");
    }
}
