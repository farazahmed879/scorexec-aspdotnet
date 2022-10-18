using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;
using ScoringAppReact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoringAppReact.FallOfWickets.Repository
{
    public class FallofWicketRepository : IFallofWicketRepository
    {
        private readonly IRepository<FallOfWicket, long> _repository;

        public FallofWicketRepository(IRepository<FallOfWicket, long> repository)
        {
            _repository = repository;
        }


        public async Task<FallOfWicket> Get(long? id = null, long? matchId = null, long? teamId = null, long? playerId = null)
        {
            return await _repository.GetAll().Where(i =>
            (!id.HasValue || i.Id == id) &&
            (!matchId.HasValue || i.MatchId == matchId) &&
            (!teamId.HasValue || i.TeamId == teamId) &&
            (!playerId.HasValue || i.PlayerId == playerId)
            )
                .FirstOrDefaultAsync();
        }


        public void InsertOrUpdateRange(List<FallOfWicket> models)
        {
            _repository.GetDbContext().UpdateRange(models);
        }

        public async Task<FallOfWicket> Update(FallOfWicket model)
        {
            return await _repository.UpdateAsync(model);

        }

        public async Task<FallOfWicket> Insert(FallOfWicket model)
        {
            return await _repository.InsertAsync(model);

        }
    }
}
