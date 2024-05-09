using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;
using ScoringAppReact.Models;
using ScoringAppReact.Players.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoringAppReact.MatchDetails.Repository
{
    public class MatchDetailsRepository : IMatchDetailsRepository
    {
        private readonly IRepository<MatchDetail, long> _repository;
        public MatchDetailsRepository(IRepository<MatchDetail, long> repository)
        {
            _repository = repository;
        }


        public async Task<List<MatchDetail>> GetAll(int? tenantId)
        {
            var result = await _repository.GetAll()
                .Where(i => i.IsDeleted == false)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<MatchDetail>> GetAllPaginated(PagedPlayerResultRequestDto input)
        {
            return _repository.GetAll()
                .Where(i => i.IsDeleted == false);
        }


        public async Task<MatchDetail> Get(long? id , long? matchId)
        {
            var result = await _repository.GetAll()
                .Where(i => !id.HasValue || i.Id == id && !matchId.HasValue || i.MatchId == matchId && i.IsDeleted == false)
                .FirstOrDefaultAsync();
            return result;
        }


        public void InsertOrUpdateRange(List<MatchDetail> models)
        {
            _repository.GetDbContext().UpdateRange(models);
        }

        public async Task<MatchDetail> Update(MatchDetail model)
        {
            return await _repository.UpdateAsync(model);

        }

        public async Task<MatchDetail> Insert(MatchDetail model)
        {
            return await _repository.InsertAsync(model);

        }
    }
}
