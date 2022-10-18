using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using ScoringAppReact.Authorization;
using Microsoft.EntityFrameworkCore;
using ScoringAppReact.Models;
using Abp;
using Abp.Runtime.Session;
using Abp.UI;
using ScoringAppReact.TeamScores.Dto;
using ScoringAppReact.Players.Dto;
using ScoringAppReact.FallOfWickets.Dto;
using System;
using ScoringAppReact.FallOfWickets.Repository;

namespace ScoringAppReact.FallOfWickets
{
    [AbpAuthorize(PermissionNames.Pages_Roles)]
    public class FallofWicketAppService : AbpServiceBase, IFallofWicketAppService
    {
        private readonly IFallofWicketRepository _repository;
        private readonly IAbpSession _abpSession;

        public FallofWicketAppService(IFallofWicketRepository repository, IAbpSession abpSession)
        {
            _repository = repository;
            _abpSession = abpSession;
        }

        public async Task<ResponseMessageDto> CreateOrEditAsync(CreateOrUpdateFallofWicketDto model)
        {
            ResponseMessageDto result;
            if (model.Id == 0 || model.Id == null)
            {
                result = await CreateAsync(model);
            }
            else
            {
                result = await UpdateAsync(model);
            }
            return result;
        }

        private async Task<ResponseMessageDto> CreateAsync(CreateOrUpdateFallofWicketDto model)
        {
            var alreadyAdded = await _repository.Get(null,model.MatchId,model.TeamId,model.PlayerId);
            if (alreadyAdded != null)
            {
                throw new UserFriendlyException("Already added with associated team and match");
            }


            var result = await _repository.Insert(new FallOfWicket()
            {
                MatchId = model.MatchId,
                TeamId = model.TeamId,
                Runs = model.Runs,
                WicketNo = model.WicketNo,
                PlayerId = model.PlayerId,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                TenantId = _abpSession.TenantId
            });

            await UnitOfWorkManager.Current.SaveChangesAsync();

            if (result.Id != 0)
            {
                return new ResponseMessageDto()
                {
                    Id = result.Id,
                    SuccessMessage = AppConsts.SuccessfullyInserted,
                    Success = true,
                    Error = false,
                    array = new List<Object> { result }
                };
            }
            return new ResponseMessageDto()
            {
                Id = 0,
                ErrorMessage = AppConsts.InsertFailure,
                Success = false,
                Error = true,
            };
        }

        private async Task<ResponseMessageDto> UpdateAsync(CreateOrUpdateFallofWicketDto model)
        {
            var result = await _repository.Update(new FallOfWicket()
            {
                Id = model.Id.Value,
                Runs = model.Runs,
                WicketNo = model.WicketNo,
                PlayerId = model.PlayerId,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                MatchId = model.MatchId,
                TeamId = model.TeamId,
                TenantId = _abpSession.TenantId
            });

            if (result != null)
            {
                return new ResponseMessageDto()
                {
                    Id = result.Id,
                    SuccessMessage = AppConsts.SuccessfullyUpdated,
                    Success = true,
                    Error = false,
                    array = new List<Object> { result }
                };
            }
            return new ResponseMessageDto()
            {
                Id = 0,
                ErrorMessage = AppConsts.UpdateFailure,
                Success = false,
                Error = true,
            };
        }

        public async Task<FallofWicketsDto> GetById(long id)
        {
            var result = await _repository.Get(id, null, null, null);
            return ObjectMapper.Map<FallofWicketsDto>(result);
        }

        public async Task<ResponseMessageDto> DeleteAsync(long id)
        {
            if (id == 0)
            {
                throw new UserFriendlyException("Id id required");
                //return;
            }
            var model = await _repository.Get(id, null, null, null);

            if (model == null)
            {
                throw new UserFriendlyException("No record found with associated Id");
                //return;
            }
            model.IsDeleted = true;
            var result = await _repository.Update(model);

            return new ResponseMessageDto()
            {
                Id = id,
                SuccessMessage = AppConsts.SuccessfullyDeleted,
                Success = true,
                Error = false,
            };
        }

        public async Task<List<FallofWicketsDto>> GetByTeamIdAndMatchId(long teamId, long matchId)
        {
            var result = await _repository.Get(null, matchId, teamId, null);

            var ob = ObjectMapper.Map<FallofWicketsDto>(result);
            var model = new List<FallofWicketsDto>();
            model.Add(ob);
            return model;
        }
    }
}


