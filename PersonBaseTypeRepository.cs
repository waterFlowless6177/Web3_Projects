using ICD.Base.Domain.Entity;
using ICD.Base.RepositoryContract;
using ICD.Framework.Data.Repository;
using ICD.Framework.DataAnnotation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICD.Base.Repository
{
    [Dependency(typeof(IPersonBaseTypeRepository))]
    public class PersonBaseTypeRepository : BaseRepository<PersonBaseTypeEntity, int>, IPersonBaseTypeRepository
    {

    }
}
