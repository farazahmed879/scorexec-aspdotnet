﻿using System.Collections.Generic;
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
using ScoringAppReact.Players.Dto;
using System;
using Abp.Runtime.Session;

namespace ScoringAppReact.PlayerScores
{
    [AbpAuthorize(PermissionNames.Pages_Roles)]
    public class PlayerScoreAppService : AbpServiceBase, IPlayerScoreAppService
    {
        private readonly IRepository<PlayerScore, long> _repository;
        private readonly IAbpSession _abpSession;

        public PlayerScoreAppService(IRepository<PlayerScore, long> repository, IAbpSession abpSession)
        {
            _repository = repository;
            _abpSession = abpSession;
        }

        public async Task<ResponseMessageDto> CreateOrEditAsync(CreateOrUpdatePlayerScoreDto model)
        {
            ResponseMessageDto result;
            if (model.Id == 0 || model.Id == null)
            {
                result = await CreatePlayerAsync(model);
            }
            else
            {
                result = await UpdatePlayerAsync(model);
            }
            return result;
        }

        private async Task<ResponseMessageDto> CreatePlayerAsync(CreateOrUpdatePlayerScoreDto model)
        {
            //if (string.IsNullOrEmpty(model.Name))
            //{
            //    Console.WriteLine("PLayer Name Missing");
            //    //return;
            //}


            var result = await _repository.InsertAsync(new PlayerScore()
            {
                PlayerId = model.PlayerId,
                Position = model.Position,
                MatchId = model.MatchId,
                TeamId = model.TeamId,
                BowlerId = model.BowlerId,
                Bat_Runs = model.Bat_Runs,
                Bat_Balls = model.Bat_Balls,
                HowOutId = model.HowOutId,
                Ball_Runs = model.Ball_Runs,
                Overs = model.Overs,
                Wickets = model.Wickets,
                Catches = model.Catches,
                Stump = model.Stump,
                Maiden = model.Maiden,
                RunOut = model.RunOut,
                Four = model.Four,
                Six = model.Six,
                Fielder = model.Fielder,
                IsPlayedInning = model.IsPlayedInning

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

        private async Task<ResponseMessageDto> UpdatePlayerAsync(CreateOrUpdatePlayerScoreDto model)
        {
            var result = await _repository.UpdateAsync(new PlayerScore()
            {
                Id = model.Id.Value,
                PlayerId = model.PlayerId,
                Position = model.Position,
                MatchId = model.MatchId,
                TeamId = model.TeamId,
                BowlerId = model.BowlerId,
                Bat_Runs = model.Bat_Runs,
                Bat_Balls = model.Bat_Balls,
                HowOutId = model.HowOutId,
                Ball_Runs = model.Ball_Runs,
                Overs = model.Overs,
                Wickets = model.Wickets,
                Catches = model.Catches,
                Stump = model.Stump,
                Maiden = model.Maiden,
                RunOut = model.RunOut,
                Four = model.Four,
                Six = model.Six,
                Fielder = model.Fielder,
                IsPlayedInning = model.IsPlayedInning
            });

            if (result != null)
            {
                return new ResponseMessageDto()
                {
                    Id = result.Id,
                    SuccessMessage = AppConsts.SuccessfullyUpdated,
                    Success = true,
                    Error = false,
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

        public async Task<PlayerScoreDto> GetById(long id)
        {
            var result = await _repository.GetAll()
                .FirstOrDefaultAsync(i => i.Id == id);
            return ObjectMapper.Map<PlayerScoreDto>(result);
        }



        public async Task<ResponseMessageDto> DeleteAsync(long playerId)
        {
            var model = await _repository.FirstOrDefaultAsync(i => i.Id == playerId);
            model.IsDeleted = true;
            var result = await _repository.UpdateAsync(model);

            return new ResponseMessageDto()
            {
                Id = playerId,
                SuccessMessage = AppConsts.SuccessfullyDeleted,
                Success = true,
                Error = false,
            };
        }

        public async Task<List<PlayerScoreDto>> GetAll(long teamId, long matchId)
        {
            var result = await _repository.GetAll().
                Where(i => i.IsDeleted == false &&
                i.TenantId == _abpSession.TenantId &&
                i.TeamId == teamId && i.MatchId == matchId)
                .ToListAsync();
            return ObjectMapper.Map<List<PlayerScoreDto>>(result);
        }
        private async Task<PagedResultDto<PlayerScoreDto>> GetPaginatedAllAsync(PagedPlayerResultRequestDto input)
        {
            var filteredPlayers = _repository.GetAll()
                .Where(i => i.IsDeleted == false && (!input.TenantId.HasValue || i.TenantId == input.TenantId));

            var pagedAndFilteredPlayers = filteredPlayers.PageBy(input);
            //.OrderBy(i => i.Name)


            var totalCount = filteredPlayers.Count();

            return new PagedResultDto<PlayerScoreDto>(
                totalCount: totalCount,
                items: await pagedAndFilteredPlayers.Select(i => new PlayerScoreDto()
                {
                    Id = i.Id
                }).ToListAsync());
        }
    }
}

