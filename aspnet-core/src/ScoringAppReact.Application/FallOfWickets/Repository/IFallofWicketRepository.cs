using ScoringAppReact.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScoringAppReact.FallOfWickets.Repository
{
    public interface IFallofWicketRepository
    {

        Task<FallOfWicket> Get(long? id = null, long? matchId = null, long? teamId = null, long? playerId = null);
        void InsertOrUpdateRange(List<FallOfWicket> models);

        Task<FallOfWicket> Update(FallOfWicket model);

        Task<FallOfWicket> Insert(FallOfWicket model);
    }
}
