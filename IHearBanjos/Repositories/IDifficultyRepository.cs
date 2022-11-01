using IHearBanjos.Models;
using System.Collections.Generic;

namespace IHearBanjos.Repositories
{
    public interface IDifficultyRepository
    {
        List<Difficulty> GetAllDifficulties();
    }
}
