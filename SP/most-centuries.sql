Alter PROCEDURE [usp_GetMostCenturies]
@paramTeamId AS INT,
@paramSeason As Int,
@paramOvers As Int,
@paramPosition As Int, 
@paramMatchType As Int,
@paramTournamentId As Int,
@paramPlayerRoleId As Int

AS
BEGIN
	SELECT  top 10
			count (PlayerScores.MatchId) as 'TotalMatch',
			count (case when IsPlayedInning = 1 then 1 else null end) as 'TotalInnings',
			COUNT(CASE WHEN Bat_Runs >= 100 THEN 1 ELSE NULL END) AS 'NumberOf100s',
			Case When Players.[FileName] is null  then  'noImage.jpg' else Players.[FileName] end  AS 'Image',
			Players.[Name] AS 'PlayerName'
			
	
	FROM PlayerScores
	Inner join Players ON PlayerScores.PlayerId = Players.Id
	Inner join Matches ON PlayerScores.MatchId = Matches.Id
	left join [Events] On Matches.EventId = [Events].Id
	
	
	
	WHERE (@paramSeason IS NUll OR Matches.Season = @paramSeason)	And
		  (@paramOvers IS NUll OR Matches.MatchOvers = @paramOvers)	And
		  (@paramPosition IS NULL OR PlayerScores.Position = @paramPosition) And 
		  (@paramMatchType IS NULL OR Matches.MatchTypeId = @paramMatchType) And  
		  (@paramPlayerRoleId IS NUll OR Players.PlayerRoleId = @paramPlayerRoleId) And
		  (@paramTournamentId IS NUll OR [Events].Id = @paramTournamentId) And
		    (Players.IsDeactivated != 1) and 
			(IsGuestorRegistered != 'Guest' or IsGuestorRegistered is null)
	
	
	GROUP BY PlayerScores.PlayerId,
			Players.[Name],
			Players.[FileName]
		--	PlayerScores.Bat_Runs

	order by COUNT(CASE WHEN Bat_Runs >= 100 THEN 1 ELSE NULL END) desc ;
END
go