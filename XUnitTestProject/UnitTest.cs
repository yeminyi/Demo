using CodeTestDemo.Api;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using CodeTestDemo.Infrastructure.Resources;
using CodeTestDemo.Api.Controllers;
using CodeTestDemo.Core.Interfaces;
using Castle.Core.Logging;
using AutoMapper;
using CodeTestDemo.Infrastructure.Services;
using Microsoft.Extensions.Logging;
namespace XUnitTestProject
{
    public class ValuesTests
    {
        public ValuesTests()
        {
            var server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>());
            Client = server.CreateClient();
        }

        public HttpClient Client { get; }

        #region snippet_ApiScoreControllerTests1
        [Fact]
        public async Task GetList_InternalServerError()
        {
            // Arrange

            // Act
            var response = await Client.GetAsync("https://localhost:6001/api/scores");

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

        }
        #endregion

        #region snippet_ApiScoreControllerTests2
        [Fact]
        public async Task Create_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange & Act
            var mockIScoreRepo = new Mock<IScoreRepository>();
            var mockIUnitOfWorkRepo = new Mock<IUnitOfWork>();
            var mockILoggerRepo = new Mock<ILogger<ScoreController>>();
            var mockIMapperRepo = new Mock<IMapper>();
            var mockIUrlHelperRepo = new Mock<IUrlHelper>();
            var mockITypeHelperServiceRepo = new Mock<ITypeHelperService>();
            var mockIPropertyMappingContainerRepo = new Mock<IPropertyMappingContainer>();
   
            var controller = new ScoreController(
               mockIScoreRepo.Object,
               mockIUnitOfWorkRepo.Object,
               mockILoggerRepo.Object, 
               mockIMapperRepo.Object, 
               mockIUrlHelperRepo.Object,
               mockITypeHelperServiceRepo.Object, 
               mockIPropertyMappingContainerRepo.Object);
            
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.Score(scoreAddResource: null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion
       
    }
}
