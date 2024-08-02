using ICD.Framework.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICD.Base.Domain.Entity;
using ICD.Base.RepositoryContract;
using ICD.Framework.DataAnnotation;
using ICD.Framework.Extensions;
using ICD.Framework.Model;
using ICD.Framework.QueryDataSource;
using ICD.Base.Domain.View;

namespace ICD.Base.Repository
{
    [Dependency(typeof(IMatchRepository))]
    public class MatchRepository : BaseRepository<MatchEntity, int>, IMatchRepository
    {
        public async Task<ListQueryResult<MatchEntity>> GetAllMatchAsync(QueryDataSource<MatchEntity> queryDataSource)
        {
            var Result = new ListQueryResult<MatchEntity>
            {
                Entities = new List<MatchEntity>()
            };

            var queryResult = from pt in GenericRepository.Query<MatchEntity>()
                select pt;

            Result = await queryResult.ToListQueryResultAsync(queryDataSource);

            return Result;
        }

        public async Task<ListQueryResult<MatchView>> GetTeamNameInMatchAsync(QueryDataSource<MatchView> queryDataSource)
        {
            var Result = new ListQueryResult<MatchView>
            {
                Entities = new List<MatchView>()
            };

            var queryResult = from pt in GenericRepository.Query<MatchEntity>()
                join ch in GenericRepository.Query<ChampionshipEntity>() on pt.ChampionshipId equals ch.Key
                join ct in GenericRepository.Query<TeamEntity>() on pt.HomeTeamId equals ct.Key
                join ctt in GenericRepository.Query<TeamEntity>() on pt.AwayTeamId equals ctt.Key
                select new MatchView
                {
                    Key = ch.Key,
                    Date = pt.Date,
                    Location = pt.Location,
                    HomeTeamName = ct.Name,
                    AwayTeamName = ctt.Name
                };

            Result = await queryResult.ToListQueryResultAsync(queryDataSource);

            return Result;

        }
    }
}
