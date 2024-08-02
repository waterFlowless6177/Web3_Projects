using ICD.Base.Domain.Entity;
using ICD.Base.RepositoryContract;
using ICD.Framework.Data.Repository;
using ICD.Framework.DataAnnotation;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using ICD.Framework.Model;
using ICD.Framework.QueryDataSource;
using System.Threading.Tasks;
using ICD.Framework.Extensions;

namespace ICD.Base.Repository
{
    [Dependency(typeof(IPersonIdentityRepository))]
    public class PersonIdentityRepository : BaseRepository<PersonIdentityEntity, int>, IPersonIdentityRepository
    {
    }
}
