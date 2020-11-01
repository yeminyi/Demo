using System.Collections.Generic;
using System.Threading.Tasks;
using CodeTestDemo.Core.Entities;

namespace CodeTestDemo.Core.Interfaces
{
    public interface IScoreRepository
    {
        Task<PaginatedList<Score>> GetAllScoresAsync(ScoreParameters scoreParameters);
        Task<Score> GetScoreByIdAsync(int id);
        void AddScore(Score score);
    }
}