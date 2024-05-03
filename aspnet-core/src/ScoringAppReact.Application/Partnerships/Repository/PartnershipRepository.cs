using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using ScoringAppReact.Models;
using System.Threading.Tasks;
using Abp.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Abp.EntityFrameworkCore.Extensions;

namespace ScoringAppReact.Partnerships.Repository
{
    public class PartnershipRepository : IPartnershipRepository
    {
        private readonly IRepository<Partnership, long> _repository;
        public PartnershipRepository(IRepository<Partnership, long> repository)
        {
            _repository = repository;
        }

        public async Task<Partnership> Get(long? id, long? matchId, long? playerId, long? teamId, long? tenantId)
        {
            var result = await _repository.GetAll()
               .FirstOrDefaultAsync(i => i.Id == id && i.IsDeleted == false &&
               (!tenantId.HasValue || i.TenantId == tenantId) &&
               (!matchId.HasValue || i.MatchId == matchId) &&
               (!teamId.HasValue || i.TeamId == teamId) &&
               (!playerId.HasValue || i.Player1Id == playerId || i.Player2Id == playerId)
               );
            return result;
        }

        public async Task<List<Partnership>> GetPlayersPartnerShips(long? id, long? matchId, long player1Id, long player2Id, long? teamId, long? tenantId)
        {
            var result = await _repository.GetAll()
               .Where(i => i.Id == id && i.IsDeleted == false &&
               (!tenantId.HasValue || i.TenantId == tenantId) &&
               (!matchId.HasValue || i.MatchId == matchId) &&
               (!teamId.HasValue || i.TeamId == teamId) &&
               ((i.Player1Id == player1Id || i.Player2Id == player1Id) && (i.Player1Id == player2Id || i.Player2Id == player2Id))
               ).ToListAsync();
            return result;
        }

        public async Task<Partnership> GetPlayersPartnerShipInSingleMatch(long? id, long matchId, long? player1Id, long? player2Id, long teamId, long? tenantId)
        {
            var m = await _repository.GetAll()
                
                .FirstOrDefaultAsync();
            
            
            var result = await _repository.GetAll()

                .IncludeIf(player1Id.HasValue, i => i.Player1)
                .IncludeIf(player2Id.HasValue, i => i.Player2)
               .Where(i => (!id.HasValue || i.Id == id) && i.IsDeleted == false &&
               (!tenantId.HasValue || i.TenantId == tenantId) &&
               (i.MatchId == matchId && i.TeamId == teamId &&
               (i.Player1Id == player1Id || i.Player2Id == player1Id) && (i.Player1Id == player2Id || i.Player2Id == player2Id))
               ).FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<Partnership>> GetAll(long? id, long? matchId, long? playerId, long? teamId, long? tenantId)
        {
            var result = await _repository.GetAll()
                .Where(i => i.Id == id && i.IsDeleted == false &&
               (!tenantId.HasValue || i.TenantId == tenantId) &&
               (!matchId.HasValue || i.MatchId == matchId) &&
               (!teamId.HasValue || i.TeamId == teamId) &&
               (!playerId.HasValue || i.Player1Id == playerId || i.Player2Id == playerId))
               .ToListAsync();
            return result;
        }

        public void InsertOrUpdateRange(List<Partnership> models)
        {
            _repository.GetDbContext().UpdateRange(models);
        }

        public async Task<Partnership> Update(Partnership model)
        {
            return await _repository.UpdateAsync(model);

        }

        public async Task<Partnership> Insert(Partnership model)
        {
            return await _repository.InsertAsync(model);

        }


    }
}
