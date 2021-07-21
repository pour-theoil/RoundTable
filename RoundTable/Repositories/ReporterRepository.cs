﻿using Microsoft.Data.SqlClient;
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
    public class ReporterRepository : BaseRepository, IReporterRepository
    {

        public ReporterRepository(IConfiguration config) : base(config) { }

        public Reporter GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                     SELECT Id, Email, FirebaseId, Firstname, LastName, Organization, Phone
                                    FROM Reporter
                                    WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@id", id);

                    Reporter reporter = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        reporter = new Reporter
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Email = DbUtils.GetString(reader, "Email"),
                            FirstName = DbUtils.GetString(reader, "Firstname"),
                            LastName = DbUtils.GetString(reader, "LastName"),
                            Organization = DbUtils.GetString(reader, "Organization"),
                            Phone = DbUtils.GetString(reader, "Phone"),
                            FirebaseId = DbUtils.GetString(reader, "FirebaseId"),
                        };
                    }
                    reader.Close();

                    return reporter;
                }
            }
        }

        public Reporter GetByFirebaseUserId(string firebaseId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                    SELECT Id, Email, FirebaseId, Firstname, LastName, Organization, Phone 
                                    FROM Reporter
                                    WHERE FirebaseId = @FirebaseId";

                    cmd.Parameters.AddWithValue("@FirebaseId", firebaseId);

                    Reporter reporter = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        reporter = new Reporter
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Email = DbUtils.GetString(reader, "Email"),
                            FirstName = DbUtils.GetString(reader, "Firstname"),
                            LastName = DbUtils.GetString(reader, "LastName"),
                            Organization = DbUtils.GetString(reader, "Organization"),
                            Phone = DbUtils.GetString(reader, "Phone"),
                            FirebaseId = DbUtils.GetString(reader, "FirebaseId"),
                        };
                    }
                    reader.Close();

                    return reporter;
                }
            }
        }

        public void Add(Reporter reporter)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        INSERT INTO
                                        Reporter (Email, FirebaseId, Firstname, LastName, Organization, Phone) 
                                        OUTPUT INSERTED.ID
                                        VALUES(@email, @FirebaseId, @Firstname, @LastName, @Organization, @Phone)";

                    DbUtils.AddParameter(cmd, "@email", reporter.Email);
                    DbUtils.AddParameter(cmd, "@FirebaseId", reporter.FirebaseId);
                    DbUtils.AddParameter(cmd, "@Firstname", reporter.FirstName);
                    DbUtils.AddParameter(cmd, "@LastName", reporter.LastName);
                    DbUtils.AddParameter(cmd, "@Organization", reporter.Organization);
                    DbUtils.AddParameter(cmd, "@Phone", reporter.Phone);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}