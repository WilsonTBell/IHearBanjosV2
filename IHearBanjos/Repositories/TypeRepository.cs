using IHearBanjos.Models;
using IHearBanjos.Utils;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace IHearBanjos.Repositories
{
    public class TypeRepository : BaseRepository, ITypeRepository
    {
        public TypeRepository(IConfiguration configuration) : base(configuration) { }

        public List<Type> GetAllTypes()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT t.Id, t.Name
                        FROM Types t
                    ";

                    using (var reader = cmd.ExecuteReader())
                    {
                        List<Type> types = new List<Type>();

                        while (reader.Read())
                        {
                            Type type = new Type()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),                                
                                Name = DbUtils.GetString(reader, "Name"),
                            };
                            types.Add(type);
                        }

                        return types;
                    }
                }
            }
        }
    }
}
