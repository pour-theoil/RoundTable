using Microsoft.Extensions.Configuration;
using RoundTable.Models;
using RoundTable.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTable.Repositories
{
    public class StoryTypeRepository : BaseRepository, IStoryTypeRepository
    {
        public StoryTypeRepository(IConfiguration config) : base(config) { }
        
        public List<StoryType> GetAllStoryType()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select * from Type";
                    var reader = cmd.ExecuteReader();

                    var types = new List<StoryType>();
                    while (reader.Read())
                    {
                        var newtype = new StoryType()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name")
                        };
                        types.Add(newtype);
                    };
                    reader.Close();
                    return types;
                }
            }
        }

        public StoryType GetStoryTypeById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select * from Type where Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    var reader = cmd.ExecuteReader();

                    StoryType newtype = null;
                    while (reader.Read())
                    {
                        newtype = new StoryType()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name")
                        };
                       
                    };
                    reader.Close();
                    return newtype;
                }
            }
        }
    }
}
