using ScoringAppReact.Models;
using ScoringAppReact.Players.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScoringAppReact.MatchDetails.Repository
{
    public interface IMatchDetailsRepository
    {

        Task<MatchDetail> Get(long? id, long? matchId);

        Task<List<MatchDetail>> GetAll(int? tenantId);

        Task<IEnumerable<MatchDetail>> GetAllPaginated(PagedPlayerResultRequestDto input);

        void InsertOrUpdateRange(List<MatchDetail> models);

        Task<MatchDetail> Update(MatchDetail model);

        Task<MatchDetail> Insert(MatchDetail model);
    }
}
