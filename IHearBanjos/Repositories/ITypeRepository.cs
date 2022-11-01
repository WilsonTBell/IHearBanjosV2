using IHearBanjos.Models;
using System.Collections.Generic;

namespace IHearBanjos.Repositories
{
    public interface ITypeRepository
    {
        List<Type> GetAllTypes();
    }
}
