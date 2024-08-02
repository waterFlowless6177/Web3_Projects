using ICD.Base.Domain.Entity;
using ICD.Base.RepositoryContract;
using ICD.Framework.Data.Repository;
using ICD.Framework.DataAnnotation;

namespace ICD.Base.Repository
{
    [Dependency(typeof(IPersonTitleLanguageRepository))]
    public class PersonTitleLanguageRepository : BaseRepository<PersonTitleLanguageEntity, int>, IPersonTitleLanguageRepository { }
}
