using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using IHearBanjos.Models;
using IHearBanjos.Utils;


namespace IHearBanjos.Repositories
{
    public class BanjoistRepository : BaseRepository, IBanjoistRepository
    {
        public BanjoistRepository(IConfiguration configuration) : base(configuration) { }

        public List<Banjoist> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT b. Id, b.FirebaseUserId, b.Name, b.Email, b.UserTypeId, ut.Name AS UserTypeName
                        FROM Banjoists b
                        LEFT JOIN UserTypes ut ON b.UserTypeId = ut.Id
                        ORDER BY b.Name
                    ";

                    using (var reader = cmd.ExecuteReader())
                    {
                        List<Banjoist> banjoists = new List<Banjoist>();

                        while (reader.Read())
                        {
                            Banjoist banjoist = new Banjoist()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                UserTypeId = DbUtils.GetInt(reader, "UserTypeId"),
                                UserType = new UserType()
                                {
                                    Id = DbUtils.GetInt(reader, "UserTypeId"),
                                    Name = DbUtils.GetString(reader, "Name"),
                                }
                            };
                            banjoists.Add(banjoist);
                        }

                        return banjoists;
                    }
                }
            }
        }

        public Banjoist GetByFirebaseUserId(string firebaseUserId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT b. Id, b.FirebaseUserId, b.Name, b.Email, b.UserTypeId, ut.Name AS UserTypeName
                        FROM Banjoists b
                        LEFT JOIN UserTypes ut ON b.UserTypeId = ut.Id
                        WHERE FirebaseUserId = @FirebaseuserId";

                    DbUtils.AddParameter(cmd, "@FirebaseUserId", firebaseUserId);

                    Banjoist banjoist = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        banjoist = new Banjoist()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),
                            UserTypeId = DbUtils.GetInt(reader, "UserTypeId"),
                            UserType = new UserType()
                            {
                                Id = DbUtils.GetInt(reader, "UserTypeId"),
                                Name = DbUtils.GetString(reader, "Name"),
                            }
                        };
                    }
                    reader.Close();

                    return banjoist;
                }
            }
        }

        public Banjoist GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT b. Id, b.FirebaseUserId, b.Name, b.Email, b.UserTypeId, ut.Name AS UserTypeName
                        FROM Banjoists b
                        LEFT JOIN UserTypes ut ON b.UserTypeId = ut.Id
                        WHERE b.Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Banjoist banjoist = null;
                        while (reader.Read())
                        {
                            if (banjoist == null)
                            {
                                banjoist = new Banjoist()
                                {
                                    Id = id,
                                    Name = DbUtils.GetString(reader, "Name"),
                                    Email = DbUtils.GetString(reader, "Email"),
                                    UserTypeId = DbUtils.GetInt(reader, "UserTypeId"),
                                    UserType = new UserType()
                                    {
                                        Id = DbUtils.GetInt(reader, "UserTypeId"),
                                        Name = DbUtils.GetString(reader, "Name"),
                                    }
                                };
                            }

                        }
                        return banjoist;
                    }
                }
            }
        }
        public void Add(Banjoist banjoist)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Banjoists (FirebaseUserId, Name,
                                                                 Email, UserTypeId)
                                        OUTPUT INSERTED.ID
                                        VALUES (@FirebaseUserId, @Name, @Email, @UserTypeId)";
                    DbUtils.AddParameter(cmd, "@FirebaseUserId", banjoist.FirebaseUserId);
                    DbUtils.AddParameter(cmd, "@Name", banjoist.Name);
                    DbUtils.AddParameter(cmd, "@Email", banjoist.Email);
                    DbUtils.AddParameter(cmd, "@UserTypeId", banjoist.UserTypeId);

                    banjoist.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
