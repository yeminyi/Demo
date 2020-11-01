using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeTestDemo.Core.Entities;
using CodeTestDemo.Core.Interfaces;
using CodeTestDemo.Infrastructure.Database;
using CodeTestDemo.Infrastructure.Extensions;
using CodeTestDemo.Infrastructure.Resources;
using CodeTestDemo.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace CodeTestDemo.Infrastructure.Repositories
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly MyContext _myContext;
        private readonly IPropertyMappingContainer _propertyMappingContainer;
        public ScoreRepository(MyContext myContext,
             IPropertyMappingContainer propertyMappingContainer)
        {
            _myContext = myContext;
            _propertyMappingContainer = propertyMappingContainer;
        }
  
        public async Task<PaginatedList<Score>> GetAllScoresAsync(ScoreParameters scoreParameters)
        {
            var query = _myContext.Scores.AsQueryable();

            if (!string.IsNullOrEmpty(scoreParameters.GameTitle))
            {
                var title = scoreParameters.GameTitle.ToLowerInvariant();
                query = query.Where(x => x.GameTitle.ToLowerInvariant().Contains(title));
            }

            query = query.ApplySort(scoreParameters.OrderBy, _propertyMappingContainer.Resolve<ScoreResource, Score>());

            var count = await query.CountAsync();
            var data = await query
                .Skip(scoreParameters.PageIndex * scoreParameters.PageSize)
                .Take(scoreParameters.PageSize)
                .ToListAsync();

            return new PaginatedList<Score>(scoreParameters.PageIndex, scoreParameters.PageSize, count, data);
        }

        public async Task<Score> GetScoreByIdAsync(int id)
        {
            return await _myContext.Scores.FindAsync(id);
        }
        public void AddScore(Score score)
        {
            _myContext.Scores.Add(score);
        }

    }
}
