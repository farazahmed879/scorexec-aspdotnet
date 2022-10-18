using ScoringAppReact.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScoringAppReact.Partnerships.Repository
{
    public interface IPartnershipRepository
    {

        Task<Partnership> Get(long? id, long? matchId, long? playerId, long? teamId, long? tenantId);

        Task<Partnership> GetPlayersPartnerShipInSingleMatch(long? id, long matchId, long? player1Id, long? player2Id, long teamId, long? tenantId);

        Task<List<Partnership>> GetPlayersPartnerShips(long? id, long? matchId, long player1Id, long player2Id, long? teamId, long? tenantId);

        Task<List<Partnership>> GetAll(long? id, long? matchId, long? playerId, long? teamId, long? tenantId);

        void InsertOrUpdateRange(List<Partnership> models);

        Task<Partnership> Update(Partnership model);

        Task<Partnership> Insert(Partnership model);

    }
}
