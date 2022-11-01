using Azure;
using IHearBanjos.Models;
using IHearBanjos.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace IHearBanjos.Repositories
{
    public class TabRepository : BaseRepository, ITabRepository
    {
        public TabRepository(IConfiguration configuration) : base(configuration) { }
        public List<Tab> GetAllTabs()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT  t.Id, t.Title, t.TypeId, t.DifficultyId, t.BanjoistId,
                                t.Description, t.ImageLocation,
                                b.Name, b.UserTypeId, b.FirebaseUserId,
                                b.Email,
                                ty.Name AS Type, d.Name as Difficulty, ut.Name as UserType
                        FROM Tabs t
                        LEFT JOIN Banjoists b ON t.BanjoistId = b.Id
                        LEFT JOIN Types ty ON t.TypeId = ty.Id
                        LEFT JOIN Difficulties d ON t.DifficultyId = d.Id
                        LEFT JOIN UserTypes ut ON b.UserTypeId = ut.Id
                       ";

                    var reader = cmd.ExecuteReader();

                    var tabs = new List<Tab>();

                    while (reader.Read())
                    {
                        tabs.Add(NewTabFromReader(reader));
                    };
                    reader.Close();
                    return tabs;
                }
            }
        }

        public Tab GetTabById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT  t.Id, t.Title, t.TypeId, t.DifficultyId, t.BanjoistId,
                                t.Description, t.ImageLocation,
                                b.Name, b.UserTypeId, b.FirebaseUserId,
                                b.Email,
                                ty.Name AS Type,  d.Name as Difficulty, ut.Name as UserType
                        FROM Tabs t
                        LEFT JOIN Banjoists b ON t.BanjoistId = b.Id
                        LEFT JOIN Types ty ON t.TypeId = ty.Id
                        LEFT JOIN Difficulties d ON t.DifficultyId = d.Id
                        LEFT JOIN UserTypes ut ON b.UserTypeId = ut.Id
                        WHERE t.ID = @id"
                    ;

                    DbUtils.AddParameter(cmd, "@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Tab tab = null;
                        if (reader.Read())
                        {
                            tab = NewTabFromReader(reader);
                        }
                        reader.Close();
                        return tab;
                    }
                }
            }
        }

        public List<Tab> GetTabsByBanjoistId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT  t.Id, t.Title, t.TypeId, t.DifficultyId, t.BanjoistId,
                                t.Description, t.ImageLocation,
                                b.Name, b.UserTypeId, b.FirebaseUserId,
                                b.Email,
                                ty.Name AS Type, d.Name as Difficulty, ut.Name as UserType
                        FROM Tabs t
                        LEFT JOIN Banjoists b ON t.BanjoistId = b.Id
                        LEFT JOIN Types ty ON t.TypeId = ty.Id
                        LEFT JOIN Difficulties d ON t.DifficultyId = d.Id
                        LEFT JOIN UserTypes ut ON b.UserTypeId = ut.Id
                        WHERE t.BanjoistId = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    var reader = cmd.ExecuteReader();

                    var tabs = new List<Tab>();

                    while (reader.Read())
                    {
                        tabs.Add(new Tab()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            Title = DbUtils.GetString(reader, "title"),
                            Description = DbUtils.GetString(reader, "description"),
                            ImageLocation = DbUtils.GetString(reader, "imageLocation"),
                            TypeId = DbUtils.GetInt(reader, "typeId"),
                            Type = new Type()
                            {
                                Name = DbUtils.GetString(reader, "type")
                            },
                            DifficultyId = DbUtils.GetInt(reader, "difficultyId"),
                            Difficulty = new Difficulty()
                            {
                                Name = DbUtils.GetString(reader, "difficulty")
                            },
                            BanjoistId = DbUtils.GetInt(reader, "banjoistId"),
                            Banjoist = new Banjoist()
                            {
                                Id = DbUtils.GetInt(reader, "banjoistId"),
                                FirebaseUserId = DbUtils.GetString(reader, "firebaseUserId"),
                                Name = DbUtils.GetString(reader, "name"),
                                Email = DbUtils.GetString(reader, "email"),
                                UserTypeId = DbUtils.GetInt(reader, "userTypeId"),
                                UserType = new UserType()
                                {
                                    Id = DbUtils.GetInt(reader, "userTypeId"),
                                    Name = DbUtils.GetString(reader, "userType")
                                }
                            }
                        });
                    };
                    reader.Close();
                    return tabs;
                }
            }
        }

        public void Add(Tab tab)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Tabs (Title, TypeId, DifficultyId, BanjoistId, Description, ImageLocation)
                                        OUTPUT INSERTED.ID
                                        VALUES (@Title, @TypeId, @DifficultyId, @BanjoistId, @Description, @ImageLocation)";
                    DbUtils.AddParameter(cmd, "@Title", tab.Title);
                    DbUtils.AddParameter(cmd, "@TypeId", tab.TypeId);
                    DbUtils.AddParameter(cmd, "@DifficultyId", tab.DifficultyId);
                    DbUtils.AddParameter(cmd, "@BanjoistId", tab.BanjoistId);
                    DbUtils.AddParameter(cmd, "@Description", tab.Description);
                    DbUtils.AddParameter(cmd, "@ImageLocation", tab.ImageLocation);
                    tab.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public List<Tab> GetBanjoistFavoriteTabs(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT  bf.Id as FavoriteId, t.Id, t.Title, t.TypeId, t.DifficultyId, t.BanjoistId,
                                t.Description, t.ImageLocation,
                                b.Name, b.UserTypeId, b.FirebaseUserId,
                                b.Email,
                                ty.Name AS Type, d.Name as Difficulty, ut.Name as UserType
                        FROM BanjoistFavorites bf
                        LEFT JOIN Tabs t ON bf.TabId = t.Id
                        LEFT JOIN Banjoists b ON t.BanjoistId = b.Id
                        LEFT JOIN Types ty ON t.TypeId = ty.Id
                        LEFT JOIN Difficulties d ON t.DifficultyId = d.Id
                        LEFT JOIN UserTypes ut ON b.UserTypeId = ut.Id
                        WHERE bf.BanjoistId = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    var reader = cmd.ExecuteReader();

                    var tabs = new List<Tab>();

                    while (reader.Read())
                    {
                        tabs.Add(new Tab()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            Title = DbUtils.GetString(reader, "title"),
                            Description = DbUtils.GetString(reader, "description"),
                            ImageLocation = DbUtils.GetString(reader, "imageLocation"),
                            TypeId = DbUtils.GetInt(reader, "typeId"),
                            Type = new Type()
                            {
                                Name = DbUtils.GetString(reader, "type")
                            },
                            DifficultyId = DbUtils.GetInt(reader, "difficultyId"),
                            Difficulty = new Difficulty()
                            {
                                Name = DbUtils.GetString(reader, "difficulty")
                            },
                            BanjoistId = DbUtils.GetInt(reader, "banjoistId"),
                            Banjoist = new Banjoist()
                            {
                                Id = DbUtils.GetInt(reader, "banjoistId"),
                                FirebaseUserId = DbUtils.GetString(reader, "firebaseUserId"),
                                Name = DbUtils.GetString(reader, "name"),
                                Email = DbUtils.GetString(reader, "email"),
                                UserTypeId = DbUtils.GetInt(reader, "userTypeId"),
                                UserType = new UserType()
                                {
                                    Id = DbUtils.GetInt(reader, "userTypeId"),
                                    Name = DbUtils.GetString(reader, "userType")
                                }
                            }
                        });
                    };
                    reader.Close();
                    return tabs;
                }
            }
        }

        public void AddFavorite(BanjoistFavorite banjoistFavorite)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO BanjoistFavorites (BanjoistId, TabId)
                                        OUTPUT INSERTED.ID
                                        VALUES (@BanjoistId, @TabId)";
                    DbUtils.AddParameter(cmd, "@TabId", banjoistFavorite.TabId);
                    DbUtils.AddParameter(cmd, "@BanjoistId", banjoistFavorite.BanjoistId);
                    banjoistFavorite.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void DeleteFavorite(int tabId, int banjoistId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM BanjoistFavorites WHERE TabId = @TabId";
                    DbUtils.AddParameter(cmd, "@TabId", tabId);
                    DbUtils.AddParameter(cmd, "@BanjoistId", banjoistId);
                    cmd.ExecuteNonQuery();
                }
            }
        }



        public void UpdateTab(Tab tab)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Tabs
                        SET Title = @Title,
                            Description = @Description,
                            DifficultyId = @DifficultyId,
                            TypeId = @TypeId,
                            ImageLocation = @ImageLocation
                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Title", tab.Title);
                    DbUtils.AddParameter(cmd, "@TypeId", tab.TypeId);
                    DbUtils.AddParameter(cmd, "@DifficultyId", tab.DifficultyId);
                    DbUtils.AddParameter(cmd, "@BanjoistId", tab.BanjoistId);
                    DbUtils.AddParameter(cmd, "@Description", tab.Description);
                    DbUtils.AddParameter(cmd, "@ImageLocation", tab.ImageLocation);
                    DbUtils.AddParameter(cmd, "@Id", tab.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTab(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Tabs WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private Tab NewTabFromReader(SqlDataReader reader)
        {
            return new Tab()
            {
                Id = DbUtils.GetInt(reader, "id"),
                Title = DbUtils.GetString(reader, "title"),
                Description = DbUtils.GetString(reader, "description"),
                ImageLocation = DbUtils.GetString(reader, "imageLocation"),
                TypeId = DbUtils.GetInt(reader, "typeId"),
                Type = new Type()
                {
                    Name = DbUtils.GetString(reader, "type")
                },
                DifficultyId = DbUtils.GetInt(reader, "difficultyId"),
                Difficulty = new Difficulty()
                {
                    Name = DbUtils.GetString(reader, "difficulty")
                },
                BanjoistId = DbUtils.GetInt(reader, "banjoistId"),
                Banjoist = new Banjoist()
                {
                    Id = DbUtils.GetInt(reader, "banjoistId"),
                    FirebaseUserId = DbUtils.GetString(reader, "firebaseUserId"),
                    Name = DbUtils.GetString(reader, "name"),
                    Email = DbUtils.GetString(reader, "email"),
                    UserTypeId = DbUtils.GetInt(reader, "userTypeId"),
                    UserType = new UserType()
                    {
                        Id = DbUtils.GetInt(reader, "userTypeId"),
                        Name = DbUtils.GetString(reader, "userType")
                    }
                }
            };
        }
    }
}
