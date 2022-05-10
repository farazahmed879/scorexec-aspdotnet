﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ScoringAppReact.Matches.Dto;
using ScoringAppReact.Models;

namespace ScoringAppReact.Matches
{
    public interface IMatchAppService : IApplicationService
    {

        Task<ResponseMessageDto> CreateOrEditAsync(CreateOrUpdateMatchDto typeDto);

        Task<CreateOrUpdateMatchDto> GetById(long typeId);

        Task<ResponseMessageDto> DeleteAsync(long typeId);

        Task<List<MatchDto>> GetAll();

        Task<PagedResultDto<MatchDto>> GetPaginatedAllAsync(PagedMatchResultRequestDto input);

        Task<List<BracketStages>> GetTeamsOfStage(int eventId);

        Task<List<MatchDto>> GetAllMatchesBYEventId(long eventId);

        List<EventMatches> GetAllStagedMatchesByEventId(long eventId);

        Task<EventMatch> EditEventMatch(long id);

        Task<List<PlayerMatches>> GetMatchesByPlayerId(long id);

        Task<List<PlayerMatches>> GetMOMByPlayerId(long id);

        Task<List<PlayerMatches>> GetMatchesByTeamId(long id);
    }
}
