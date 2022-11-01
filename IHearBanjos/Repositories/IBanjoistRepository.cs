using IHearBanjos.Models;
using System.Collections.Generic;

namespace IHearBanjos.Repositories
{
    public interface IBanjoistRepository
    {
        List<Banjoist> GetAll();
        void Add(Banjoist banjoist);
        Banjoist GetByFirebaseUserId(string firebaseUserId);
        Banjoist GetById(int id);
    }
}
