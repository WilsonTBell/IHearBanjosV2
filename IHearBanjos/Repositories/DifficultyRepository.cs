using IHearBanjos.Models;
using IHearBanjos.Utils;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace IHearBanjos.Repositories
{
    public class DifficultyRepository: BaseRepository, IDifficultyRepository
    {
        public DifficultyRepository(IConfiguration configuration) : base(configuration) { }

        public List<Difficulty> GetAllDifficulties()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT d.Id, d.Name
                        FROM Difficulties d
                    ";

                    using (var reader = cmd.ExecuteReader())
                    {
                        List<Difficulty> difficulties = new List<Difficulty>();

                        while (reader.Read())
                        {
                            Difficulty difficulty = new Difficulty()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                            };
                            difficulties.Add(difficulty);
                        }

                        return difficulties;
                    }
                }
            }
        }
    }
}
