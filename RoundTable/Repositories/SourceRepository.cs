using Microsoft.Extensions.Configuration;
using RoundTable.Models;
using RoundTable.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTable.Repositories
{
    public class SourceRepository : BaseRepository, ISourceRepository
    {
        public SourceRepository(IConfiguration config) : base(config) { }
        public void AddSource(Source source)
        {
            throw new NotImplementedException();
        }

        public void DeleteSource(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Delete from Source where id = @id;
                                        Delete from SourceCatagory where sourceId = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Source> GetAllSouces(int reporterId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT s.id, s.firstname, s.lastname, s.email, s.organization, s.phone, s.jobtitle, s.reporterId,
                                        c.id as CategoryId, c.name as CategoryName
                                        FROM SOURCE S LEFT JOIN sourceCatagory SC ON SC.sourceId = S.id
                                        LEFT JOIN category C ON C.id = SC.categoryId
                                        WHERE IsDeleted = 0 AND reporterId =@reporterId; ";
                    DbUtils.AddParameter(cmd, "@reporterId", reporterId);
                    var sources = new List<Source>();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var sourceId = DbUtils.GetInt(reader, "Id");

                        var existingsource = sources.FirstOrDefault(s => s.Id == sourceId);
                        if (existingsource == null)
                        {
                            existingsource = new Source()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                FirstName = DbUtils.GetString(reader, "FirstName"),
                                LastName = DbUtils.GetString(reader, "LastName"),
                                Organization = DbUtils.GetString(reader, "Organization"),
                                Email = DbUtils.GetString(reader, "Email"),
                                Phone = DbUtils.GetString(reader, "Phone"),
                                JobTitle = DbUtils.GetString(reader, "Jobtitle"),
                                Categories = new List<Category>(),

                            };

                            sources.Add(existingsource);
                        }
                        if (DbUtils.IsNotDbNull(reader, "CategoryId"))
                        {
                            existingsource.Categories.Add(new Category()
                            {
                                Id = DbUtils.GetInt(reader, "CategoryId"),
                                Name = DbUtils.GetString(reader, "CategoryName")
                            });
                        }
                    }
                    reader.Close();
                    return sources;

                }
            }
        }

        public Source GetSouceById(int sourceId, int reporterId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Distinct s.id, s.firstname, s.lastname, s.email, s.organization, s.phone, s.jobtitle, s.reporterId,
                                        c.id as CategoryId, c.name as CategoryName
                                        FROM SOURCE S LEFT JOIN sourceCatagory SC ON SC.sourceId = S.id
                                        LEFT JOIN category C ON C.id = SC.categoryId
                                        WHERE s.IsDeleted = 0 AND s.reporterId =@reporterId
                                        and s.id = @sourceId; ";
                    DbUtils.AddParameter(cmd, "@sourceId", sourceId);
                    DbUtils.AddParameter(cmd, "@reporterId", reporterId);
                    Source source = null;
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                       

                        
                        if (source == null)
                        {
                            source = new Source()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                FirstName = DbUtils.GetString(reader, "FirstName"),
                                LastName = DbUtils.GetString(reader, "LastName"),
                                Organization = DbUtils.GetString(reader, "Organization"),
                                Email = DbUtils.GetString(reader, "Email"),
                                Phone = DbUtils.GetString(reader, "Phone"),
                                JobTitle = DbUtils.GetString(reader, "Jobtitle"),
                                Categories = new List<Category>(),

                            };

                            
                        }
                        if (DbUtils.IsNotDbNull(reader, "CategoryId"))
                        {
                            source.Categories.Add(new Category()
                            {
                                Id = DbUtils.GetInt(reader, "CategoryId"),
                                Name = DbUtils.GetString(reader, "CategoryName")
                            });
                        }
                    }
                    reader.Close();
                    return source;

                }
            }
        }


        public void UpdateSource(Source source)
        {
            throw new NotImplementedException();
        }
    }
}
