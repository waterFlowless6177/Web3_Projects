using ICD.Base.Domain.Entity;
using ICD.Base.Domain.View;
using ICD.Base.RepositoryContract;
using ICD.Framework.Data.Repository;
using ICD.Framework.DataAnnotation;
using ICD.Framework.Model;
using ICD.Framework.QueryDataSource;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ICD.Framework.Extensions;

namespace ICD.Base.Repository
{
    [Dependency(typeof(IPersonRepository))]
    public class PersonRepository : BaseRepository<PersonEntity, Int64>, IPersonRepository
    {
        public async Task<ListQueryResult<PersonView>> GetPeoplesByTitleRefAndLanguageRef(QueryDataSource<PersonView> searchQuery, int languageRef)
        {
            var finalResult = new ListQueryResult<PersonView>
            {
                Entities = new List<PersonView>()
            };

            var queryResult = from p in GenericRepository.Query<PersonEntity>()
                              join pl in GenericRepository.Query<PersonLanguageEntity>()
                              on p.Key equals pl.PersonRef
                              join ptl in GenericRepository.Query<PersonTitleEntity>()
                              on p.PersonTitleRef equals ptl.Key
                              join persontl in GenericRepository.Query<PersonTitleLanguageEntity>()
                              on ptl.Key equals persontl.PersonTitleRef

                              join pbt in GenericRepository.Query<PersonBaseTypeEntity>()
                              on p.Key equals pbt.PersonRef into leftPbt
                              from lPbt in leftPbt.DefaultIfEmpty()

                              join bt in GenericRepository.Query<BaseTypeEntity>()
                              on lPbt.BaseTypeRef equals bt.Key into leftbt
                              from lbt in leftbt.DefaultIfEmpty()

                              where pl.LanguageRef == languageRef && persontl.LanguageRef == languageRef
                              select new PersonView
                              {
                                  Key = p.Key,
                                  EconomicId = p.EconomicId,
                                  _FatherName = pl._FatherName,
                                  FullName = pl.FullName,
                                  _LastName = pl._LastName,
                                  _Name = pl._Name,
                                  NationalIdentity = p.NationalIdentity,
                                  PersonTitleRef = p.PersonTitleRef,
                                  Order = ptl.Order,
                                  IsLegal = ptl.IsLegal,
                                  IsActive = ptl.IsActive,
                                  Alias = ptl.Alias,
                                  _PersonTitleName = persontl._Name,
                                  BaseTypeAlias = lbt.Alias
                              };

            var result = await queryResult.ToListQueryResultAsync(searchQuery);

            finalResult = result;

            return finalResult;
        }
    }
}
