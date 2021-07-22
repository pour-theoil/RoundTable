using Microsoft.Extensions.Configuration;
using RoundTable.Models;
using RoundTable.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTable.Repositories
{
    public class StatusRepository : BaseRepository, IStatusRepository
    {
        public StatusRepository(IConfiguration config): base(config) { }
    

        public List<Status> GetAllStatus()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select * from Status";
                    var reader = cmd.ExecuteReader();

                    var statusi = new List<Status>();
                    while (reader.Read())
                    {
                        var status = new Status()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name")
                        };
                        statusi.Add(status);
                    };
                    reader.Close();
                    return statusi;
                }
            }
        }

        public Status GetStatusById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select * from Status where Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    var reader = cmd.ExecuteReader();

                    Status status = null;
                    while (reader.Read())
                    {
                        status = new Status()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name")
                        };

                    };
                    reader.Close();
                    return status;
                }
            }
        }
    }
}
