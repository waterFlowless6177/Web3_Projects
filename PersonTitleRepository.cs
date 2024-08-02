using ICD.Base.Domain.Entity;
using ICD.Base.Domain.View;
using ICD.Base.RepositoryContract;
using ICD.Framework.Data.Repository;
using ICD.Framework.DataAnnotation;
using ICD.Framework.Extensions;
using ICD.Framework.Model;
using ICD.Framework.QueryDataSource;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ICD.Base.Repository
{
    [Dependency(typeof(IPersonTitleRepository))]
    public class PersonTitleRepository : BaseRepository<PersonTitleEntity, int>, IPersonTitleRepository
    {
        public async Task<ListQueryResult<PersonTitleView>> GetPersonTitlesByLanguageRef(QueryDataSource<PersonTitleView> searchQuery, int languageRef)
        {
            var result = new ListQueryResult<PersonTitleView>()
            {
                Entities = new List<PersonTitleView>()
            };

            var queryResult = from pt in GenericRepository.Query<PersonTitleEntity>()
                              join ptl in GenericRepository.Query<PersonTitleLanguageEntity>()
                              on pt.Key equals ptl.PersonTitleRef
                              join ir in GenericRepository.Query<ItemRowEntity>()
                              on pt.ItemRowRef_LegalType equals ir.Key
                              join irl in GenericRepository.Query<ItemRowLanguageEntity>()
                              on ir.Key equals irl.ItemRowRef
                              where irl.LanguageRef == languageRef && ptl.LanguageRef == languageRef
                              select new PersonTitleView
                              {
                                  Key = pt.Key,
                                  Order = pt.Order,
                                  IsLegal = pt.IsLegal,
                                  IsActive = pt.IsActive,
                                  Alias = pt.Alias,
                                  _Name = ptl._Name,
                                  Description = ptl._Description,
                                  ItemRowRef_LegalType = irl.ItemRowRef,
                                  ItemRowAlias = ir.Alias
                              };

            result = await queryResult.ToListQueryResultAsync(searchQuery);

            return result;
        }
    }
}
