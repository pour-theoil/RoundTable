using RoundTable.Controllers;
using RoundTable.Models;
using RoundTable.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RoundTable.Tests
{
    public class ReporterControllerTest
    {
        [Fact]
        public void Post_Method_Adds_A_New_Reporter()
        {
            //Arrange
            var reporterCount = 20;
            var reporters = CreateTestReporter(reporterCount);

            
        }

        private List<Reporter> CreateTestReporter(int count)
        {
            var users = new List<Reporter>();
            for (var i = 1; i <= count; i++)
            {
                users.Add(new Reporter()
                {
                    Id = i,
                    FirstName = $"First{i}",
                    LastName = $"Last{i}",
                    Email = $"user{i}@example.com",
                    FirebaseId = $"{i}",
                    Phone = $"{i}{i}{i}-{i}{i}{i}{i}",
                    Organization = $"Place{i}"
                });
            } 
            return users;
        }
    }
}
