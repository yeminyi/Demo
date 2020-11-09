using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeTestDemo.Api.Helpers;
using CodeTestDemo.Core.Entities;
using CodeTestDemo.Core.Interfaces;
using CodeTestDemo.Infrastructure.Extensions;
using CodeTestDemo.Infrastructure.Resources;
using CodeTestDemo.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CodeTestDemo.Api.Controllers
{
 
    [Route("api/scores")]
    public class ScoreController : Controller
    {
       
        private readonly IScoreRepository _scoreRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ScoreController> _logger;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;
        private readonly ITypeHelperService _typeHelperService;
        private readonly IPropertyMappingContainer _propertyMappingContainer;

        public ScoreController(
            IScoreRepository scoreRepository,
            IUnitOfWork unitOfWork,
            ILogger<ScoreController> logger,
            IMapper mapper,
            IUrlHelper urlHelper,
            ITypeHelperService typeHelperService,
            IPropertyMappingContainer propertyMappingContainer)
        {
            _scoreRepository = scoreRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _urlHelper = urlHelper;
            _typeHelperService = typeHelperService;
            _propertyMappingContainer = propertyMappingContainer;
        }
       

        [HttpGet(Name = "GetScores")]
        public async Task<IActionResult> Get(ScoreParameters scoreParameters)
        {
            //if (!_propertyMappingContainer.ValidateMappingExistsFor<ScoreResource, Score>(scoreParameters.OrderBy))
            //{
            //    return BadRequest("Can't finds fields for sorting.");
            //}

            //if (!_typeHelperService.TypeHasProperties<ScoreResource>(scoreParameters.Fields))
            //{
            //    return BadRequest("Fields not exist.");
            //}

            var scoreList = await _scoreRepository.GetAllScoresAsync(scoreParameters);

            var scoreResources = _mapper.Map<IEnumerable<Score>, IEnumerable<ScoreResource>>(scoreList);

            var result = scoreResources.ToDynamicIEnumerable(scoreParameters.Fields);

            var previousPageLink = scoreList.HasPrevious ?
                CreateScoreUri(scoreParameters, PaginationResourceUriType.PreviousPage) : null;

            var nextPageLink = scoreList.HasNext ?
                CreateScoreUri(scoreParameters, PaginationResourceUriType.NextPage) : null;

            var meta = new
            {
                Pagesize = scoreList.PageSize,
                PageIndex = scoreList.PageIndex,
                TotalItemsCount = scoreList.TotalItemsCount,
                PageCount = scoreList.PageCount,
                previousPageLink,
                nextPageLink
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(meta, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));

            return Ok(result);
        }
   


        [HttpPost(Name = "CreateScore")]
        public async Task<IActionResult> Score([FromBody] ScoreAddResource scoreAddResource)
        {
            if (scoreAddResource == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return new MyUnprocessableEntityObjectResult(ModelState);
            }

            var newScore = _mapper.Map<ScoreAddResource, Score>(scoreAddResource);
            
            newScore.LastModified = DateTime.Now;

            _scoreRepository.AddScore(newScore);

            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception("Save Failed!");
            }
            return Ok();
        }




        private string CreateScoreUri(ScoreParameters parameters, PaginationResourceUriType uriType)
        {
            switch (uriType)
            {
                case PaginationResourceUriType.PreviousPage:
                    var previousParameters = new
                    {
                        pageIndex = parameters.PageIndex - 1,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields,
                        title = parameters.GameTitle
                    };
                    return _urlHelper.Link("GetScores", previousParameters);
                case PaginationResourceUriType.NextPage:
                    var nextParameters = new
                    {
                        pageIndex = parameters.PageIndex + 1,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields,
                        title = parameters.GameTitle
                    };
                    return _urlHelper.Link("GetScores", nextParameters);
                default:
                    var currentParameters = new
                    {
                        pageIndex = parameters.PageIndex,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields,
                        title = parameters.GameTitle
                    };
                    return _urlHelper.Link("GetScores", currentParameters);
            }
        }

        
    }
}
