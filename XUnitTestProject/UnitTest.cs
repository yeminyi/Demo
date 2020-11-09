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
using CodeTestDemo.Core.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

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
            var content = await response.Content.ReadAsStringAsync();
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
        #region snippet_ApiScoreControllerTests3
        [Fact]
        public async Task CreateActionResult_OkResultCreatedForSession()
        {
            // Arrange

            var mockIScoreRepo = new Mock<IScoreRepository>();
            var mockIUnitOfWorkRepo = new Mock<IUnitOfWork>();
            var mockILoggerRepo = new Mock<ILogger<ScoreController>>();
            var mockIMapperRepo = new Mock<IMapper>();
            var scoreAddtestSession = GetScoreAddTestSession();
            var scoretestSession = GetScoreTestSession();

            mockIMapperRepo.Setup(m => m.Map<Score, ScoreAddResource>(It.IsAny<Score>())).Returns(scoreAddtestSession); // mapping data
            mockIMapperRepo.Setup(m => m.Map<ScoreAddResource, Score>(It.IsAny<ScoreAddResource>())).Returns(scoretestSession); // mapping data
            mockIScoreRepo.Setup(x => x.AddScore(It.IsAny<Score>()));
            mockIUnitOfWorkRepo.Setup(x => x.SaveAsync()).Returns(Task.FromResult(true));

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

            // Act
            var result = await controller.Score(scoreAddtestSession);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);

        }
        #endregion
        #region snippet_ApiScoreControllerTests4
        [Fact]
        public async Task GetList_Success()
        {
            // Arrange

            var mockIScoreRepo = new Mock<IScoreRepository>();
            var mockIUnitOfWorkRepo = new Mock<IUnitOfWork>();
            var mockILoggerRepo = new Mock<ILogger<ScoreController>>();
            var mockIMapperRepo = new Mock<IMapper>();
            var scoreParatestSession = GetScoreParaTestSession();


            var itemMock = new Mock<Score>();
            var items = new List<Score> { itemMock.Object }; //<--IEnumerable<Score>

            var entities = new PaginatedList<Score>(0, 1, 1, items)
            {
               itemMock.Object
            };

            mockIScoreRepo.Setup(x => x.GetAllScoresAsync(It.IsAny<ScoreParameters>())).ReturnsAsync(entities);

            var mockIUrlHelperRepo = new Mock<IUrlHelper>();
            var mockITypeHelperServiceRepo = new Mock<ITypeHelperService>();
            var mockIPropertyMappingContainerRepo = new Mock<IPropertyMappingContainer>();
            mockIUnitOfWorkRepo.Setup(x => x.SaveAsync()).Returns(Task.FromResult(true));

            var controller = new ScoreController(
               mockIScoreRepo.Object,
               mockIUnitOfWorkRepo.Object,
               mockILoggerRepo.Object,
               mockIMapperRepo.Object,
               mockIUrlHelperRepo.Object,
               mockITypeHelperServiceRepo.Object,
               mockIPropertyMappingContainerRepo.Object);

            // Ensure the controller can add response headers
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act
            var result = await controller.Get(scoreParatestSession);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

        }
        #endregion
        private ScoreAddResource GetScoreAddTestSession()
        {
            var session = new ScoreAddResource()
            {

                GameTitle = "Test One",
                TeamA = "Test One",
                TeamB = "Test One",
                TeamAScore = 1,
                TeamBScore = 2,
                Employee = "Test One"

            };
            return session;
        }
        private Score GetScoreTestSession()
        {
            var session = new Score()
            {

                Id = 99,
                GameTitle = "Test One",
                TeamA = "Test One",
                TeamB = "Test One",
                TeamAScore = 1,
                TeamBScore = 2,
                Employee = "Test One",
                LastModified = DateTime.Now,

            };
            return session;
        }
        private ScoreParameters GetScoreParaTestSession()
        {
            var session = new ScoreParameters()
            {
                PageSize = 1,
                PageIndex = 0
            };
            return session;
        }
    }
}
