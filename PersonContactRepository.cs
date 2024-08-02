using ICD.Base.Domain.Entity;
using ICD.Base.Domain.View;
using ICD.Base.RepositoryContract;
using ICD.Framework.Data.Repository;
using ICD.Framework.DataAnnotation;
using ICD.Framework.Model;
using ICD.Framework.QueryDataSource;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using ICD.Framework.Extensions;

namespace ICD.Base.Repository
{
    [Dependency(typeof(IPersonContactRepository))]
    public class PersonContactRepository : BaseRepository<PersonContactEntity, int>, IPersonContactRepository
    {
        public async Task<ListQueryResult<PersonContactView>> GetPersonContactAsync(QueryDataSource<PersonContactView> searchQuery, int languageRef)
        {
            var result = new ListQueryResult<PersonContactView>
            {
                Entities = new List<PersonContactView>()
            };

            var queryResult = from pc in GenericRepository.Query<PersonContactEntity>()
                              join ctl in GenericRepository.Query<ContactTypeLanguageEntity>() on pc.ContactTypeRef equals ctl.ContactTypeRef

                              where ctl.LanguageRef == languageRef

                              select new PersonContactView
                              {
                                  Key = pc.Key,
                                  ContactInfo = pc.ContactInfo,
                                  ContactTypeRef = pc.ContactTypeRef,
                                  _ContactTypeTitle = ctl._Title,
                                  IsMain = pc.IsMain,
                                  PersonRef = pc.PersonRef
                              };

            result = await queryResult.ToListQueryResultAsync(searchQuery);

            return result;
        }
    }
}
