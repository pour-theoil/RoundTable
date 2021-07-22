using Microsoft.Extensions.Configuration;
using RoundTable.Models;
using RoundTable.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTable.Repositories
{
    public class NationalOutletRepository : BaseRepository, INationalOutletRepostitory
    {
        public NationalOutletRepository(IConfiguration config) : base(config) { }
        
        public List<NationalOutlet> GetAllNationalOutlet()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select * from NationalOutlet";
                    var reader = cmd.ExecuteReader();

                    var nationaloutlets = new List<NationalOutlet>();
                    while (reader.Read())
                    {
                        var nationalOutlet = new NationalOutlet()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name")
                        };
                        nationaloutlets.Add(nationalOutlet);
                    };
                    reader.Close();
                    return nationaloutlets;
                }
            }
        }

        public NationalOutlet GetNationalOutletById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select * from NationalOutlet where Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    var reader = cmd.ExecuteReader();

                    NationalOutlet nationalOutlet = null;
                    while (reader.Read())
                    {
                        nationalOutlet = new NationalOutlet()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name")
                        };

                    };
                    reader.Close();
                    return nationalOutlet;
                }
            }
        }
    }
}
