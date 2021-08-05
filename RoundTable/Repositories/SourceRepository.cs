using Microsoft.Data.SqlClient;
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

            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    
                     var sql = @"Insert into source (FirstName, LastName, Organization, Email, Phone, 
                                        JobTitle, reporterId) 
                                        OUTPUT INSERTED.ID
                                        values(@FirstName, @LastName, @Organization, @Email, @Phone,  
                                        @JobTitle, @reporterId);";
                    //var i = 0;
                    //if(source.Categories.Count > 0)
                    //{
                    //    foreach(var cat in source.Categories)
                    //    {
                    //        sql += @$"
                    //                Insert into sourcecategories (sourceId, categoryId) 
                    //                values (INSERTED.ID, @categoryId{i});";
                    //    }
                    //}
                    
                    cmd.CommandText = sql;
                    DbUtils.AddParameter(cmd, "@FirstName", source.FirstName);
                    DbUtils.AddParameter(cmd, "@LastName", source.LastName);
                    DbUtils.AddParameter(cmd, "@Organization", source.Organization);
                    DbUtils.AddParameter(cmd, "@Email", source.Email);
                    DbUtils.AddParameter(cmd, "@Phone", source.Phone);
                    DbUtils.AddParameter(cmd, "@JobTitle", source.JobTitle);
                    DbUtils.AddParameter(cmd, "@ReporterId", source.ReporterId);

                    //i = 0;
                    //if (source.Categories.Count > 0)
                    //{
                    //    foreach (var cat in source.Categories)
                    //    {
                    //        DbUtils.AddParameter(cmd, $"@sourceId{i}", cat.Id);
                    //    }
                    //}
                    source.Id = (int)cmd.ExecuteScalar();
                }
            }

        }

        public void DeleteSource(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Delete from Source where id = @id;
                                        Delete from SourceCategory where sourceId = @id";
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
                                        FROM SOURCE S LEFT JOIN sourceCategory SC ON SC.sourceId = S.id
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
                    cmd.CommandText = @"SELECT Distinct s.id, s.firstname, s.lastname, s.email, s.organization, s.ImageLocation, s.phone, s.jobtitle, s.reporterId,
                                        c.id as CategoryId, c.name as CategoryName
                                        FROM SOURCE S LEFT JOIN sourceCategory SC ON SC.sourceId = S.id
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
                                ImageLocation = DbUtils.GetString(reader, "ImageLocation"),
                                ReporterId = reporterId,
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
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {

                    var sql = @"Update source Set
                                    FirstName = @FirstName,
                                    LastName = @LastName,
                                    Organization = @Organization,
                                    Email = @Email, 
                                    Phone = @Phone,
                                    JobTitle = @JobTitle,
                                    reporterId = @reporterId
                                    where id = @sourceid;
                                    ";
                    var i = 0;
                    if (source.Categories.Count > 0)
                    {
                        sql += "Delete from sourceCategory where sourceId = @sourceId;";
                        foreach (var cat in source.Categories)
                        {
                            sql += @$"
                                    Insert into sourceCategory (sourceId, categoryId) 
                                    values (@sourceId, @categoryId{i});";
                            i++;
                        }
                    }

                    cmd.CommandText = sql;
                    DbUtils.AddParameter(cmd, "@FirstName", source.FirstName);
                    DbUtils.AddParameter(cmd, "@LastName", source.LastName);
                    DbUtils.AddParameter(cmd, "@Organization", source.Organization);
                    DbUtils.AddParameter(cmd, "@Email", source.Email);
                    DbUtils.AddParameter(cmd, "@Phone", source.Phone);
                    DbUtils.AddParameter(cmd, "@JobTitle", source.JobTitle);
                    DbUtils.AddParameter(cmd, "@ReporterId", source.ReporterId);
                    DbUtils.AddParameter(cmd, "@sourceId", source.Id);

                    i = 0;
                    if (source.Categories.Count > 0)
                    {
                        foreach (var cat in source.Categories)
                        {
                            DbUtils.AddParameter(cmd, $"@categoryId{i}", cat.Id);
                            i++;
                        }
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddSourceToStory(int storyId, int sourceId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Insert into storySource (sourceId, storyId) values (@sourceId, @storyId);";
                    DbUtils.AddParameter(cmd, "@sourceId", sourceId);
                    DbUtils.AddParameter(cmd, "@storyId", storyId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddImage(int id, string imagelocation)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "Update Source set ImageLocation = @imagelocation where id = @id";
                    cmd.Parameters.AddWithValue("@imagelocation", imagelocation);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
