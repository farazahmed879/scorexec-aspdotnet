﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ScoringAppReact.Players.Dto;

namespace ScoringAppReact.Players
{
    public interface IPlayerAppService : IApplicationService
    {

        Task<ResponseMessageDto> CreateOrEditAsync(CreateOrUpdatePlayerDto model);

        Task<PlayerEditDto> GetById(long id);

        Task<ResponseMessageDto> DeleteAsync(long id);

        Task<List<PlayerDto>> GetAll();

        Task<PagedResultDto<PlayerDto>> GetPaginatedAllAsync(PagedPlayerResultRequestDto input);

        Task<List<PlayerDto>> GetAllByTeamId(long teamId);
        Task<ResponseMessageDto> CreateOrUpdateTeamPlayersAsync(TeamPlayerDto model);

        Task<PlayerStatisticsDto> PlayerStatistics(PlayerStatsInput input);

        Task<PlayerStatisticsDto> GetPlayerStatistics(long playerId, int? matchType, int? season, long? teamId);

        Task<List<PlayerDto>> GetAllByMatchId(long id);

        Task<List<PlayerDto>> GetAllByEventId(long id);

        Task<List<PlayerListDto>> GetTeamPlayersByMatchId(long matchId);

        Task<List<PlayerDto>> AllPlayersByTeamIds(List<long> teamIds);
    }
}
