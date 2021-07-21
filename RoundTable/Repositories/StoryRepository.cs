using Microsoft.Data.SqlClient;
using RoundTable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RoundTable.Repositories;
using RoundTable.Utils;

namespace RoundTable.Repositories
{
    public class StoryRepository : BaseRepository, IStoryRepository
    {
        public StoryRepository(IConfiguration config) : base(config) { }

        public void AddStory(Story story)
        {
            throw new NotImplementedException();
        }

        public void DeleteStory(int id)
        {
            throw new NotImplementedException();
        }

        public List<Story> GetAll(int reporterId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select s.id, s.slug, s.storyUrl, s.summary, s.laststatusupdate,
		                                t.id as TypeId, t.name as [Type], 
                                        c.id as CategoryId, c.name as Category, 
		                                source.id as SourceId, source.name as [Source], 
                                        n.id as NationalId, n.name as [National],
		                                st.id as StatusId, st.name as Status
	                                from Story s 
	                                    join category c on c.id = s.categoryId
	                                    join type t on t.id = s.typeId
		                                join status st on st.id = s.statusId
	                                    join nationalOutlet n on n.id = s.nationalId
	                                    left join storySource sour on sour.storyId = s.id
	                                    left join source on sour.sourceId = source.id
	                                    where s.reporterId = @reporterId and s.isdeleted = 0;";
                    DbUtils.AddParameter(cmd, "@reporterId", reporterId);

                    var stories = new List<Story>();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var storyId = DbUtils.GetInt(reader, "Id");

                        var existingstory = stories.FirstOrDefault(s => s.Id == storyId);
                        if (existingstory == null)
                        {
                            existingstory = new Story()
                            {

                                Id = DbUtils.GetInt(reader, "Id"),
                                Slug = DbUtils.GetString(reader, "Slug"),
                                StoryURl = DbUtils.GetString(reader, "StoryUrl"),
                                Summary = DbUtils.GetString(reader, "Summary"),
                                StoryTypeId = DbUtils.GetInt(reader, "TypeId"),
                                StoryType = new StoryType()
                                {
                                    Id = DbUtils.GetInt(reader, "TypeId"),
                                    Name = DbUtils.GetString(reader, "Type")
                                },
                                NationalId = DbUtils.GetInt(reader, "NationalId"),
                                NationalOutlet = new NationalOutlet()
                                {
                                    Id = DbUtils.GetInt(reader, "NationalId"),
                                    Name = DbUtils.GetString(reader, "National")
                                },
                                StatusId = DbUtils.GetInt(reader, "StatusId"),
                                Status = new Status()
                                {
                                    Id = DbUtils.GetInt(reader, "StatusId"),
                                    Name = DbUtils.GetString(reader, "Status")
                                },
                                LastStatusUpdate = DbUtils.GetDateTime(reader, "laststatusupdate"),
                                Sources = new List<Source>(),
                                
                            };
                            stories.Add(existingstory);
                        }
                        if (DbUtils.IsNotDbNull(reader, "sourceId"))
                        {
                            existingstory.Sources.Add(new Source()
                            {
                                Id = DbUtils.GetInt(reader, "sourceId")
                            });
                        }
                        
                    }

                    conn.Close();
                    return stories;
                }
            }
        }

        public Story GetStoryById(int storyId, int reporterId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select s.id, s.slug, s.storyUrl, s.summary, s.laststatusupdate,
		                                t.id as TypeId, t.name as [Type], 
                                        c.id as CategoryId, c.name as Category, 
		                                source.id as SourceId, source.name as [Source], 
                                        n.id as NationalId, n.name as [National],
		                                st.name as Status
	                                from Story s 
	                                    join category c on c.id = s.categoryId
	                                    join type t on t.id = s.typeId
		                                join status st on st.id = s.statusId
	                                    join nationalOutlet n on n.id = s.nationalId
	                                    left join storySource sour on sour.storyId = s.id
	                                    left join source on sour.sourceId = source.id
	                                    where s.reporterId = @reporterId and s.isdeleted = 0 and s.id = @storyId;";
                    DbUtils.AddParameter(cmd, "@storyId", storyId);
                    DbUtils.AddParameter(cmd, "@reporterId", reporterId);

                    var story = new Story();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        story = new Story()
                        {

                            Id = DbUtils.GetInt(reader, "Id"),
                            Slug = DbUtils.GetString(reader, "Slug"),
                            StoryURl = DbUtils.GetString(reader, "StoryUrl"),
                            Summary = DbUtils.GetString(reader, "Summary"),
                            StoryTypeId = DbUtils.GetInt(reader, "TypeId"),
                            StoryType = new StoryType()
                            {
                                Id = DbUtils.GetInt(reader, "TypeId"),
                                Name = DbUtils.GetString(reader, "Type")
                            },
                            NationalId = DbUtils.GetInt(reader, "NationalId"),
                            NationalOutlet = new NationalOutlet()
                            {
                                Id = DbUtils.GetInt(reader, "NationalId"),
                                Name = DbUtils.GetString(reader, "National")
                            },
                            StatusId = DbUtils.GetInt(reader, "StatusId"),
                            Status = new Status()
                            {
                                Id = DbUtils.GetInt(reader, "StatusId"),
                                Name = DbUtils.GetString(reader, "Status")
                            },
                            LastStatusUpdate = DbUtils.GetDateTime(reader, "laststatusupdate"),
                            Sources = new List<Source>()
                        };


                        if (DbUtils.IsNotDbNull(reader, "sourceId"))
                        {
                            story.Sources.Add(new Source()
                            {
                                Id = DbUtils.GetInt(reader, "sourceId")
                            });
                        }
                    }

                    conn.Close();
                    return story;
                }
            }
        }

        public void UpdateStory(Story story)
        {
            throw new NotImplementedException();
        }
    }
}
