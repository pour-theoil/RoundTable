using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoundTable.Repositories;
using RoundTable.Models;

namespace RoundTable.Tests.Mocks
{
    class InMemoryUserRepository : IReporterRepository
    {
        private readonly List<Reporter> _data;

        public InMemoryUserRepository(List<Reporter> startingData)
        {
            _data = startingData;
        }
        public void Add(Reporter reporter)
        {
            var lastReporter = _data.Last();
            reporter.Id = lastReporter.Id + 1;
            _data.Add(reporter);
        }

        public void AddImage(int id, string imagelocation)
        {
            throw new NotImplementedException();
        }

        public Reporter GetByFirebaseUserId(string firebaseUserId)
        {
            return _data.FirstOrDefault(r => r.FirebaseId == firebaseUserId);
        }

        public Reporter GetById(int id)
        {
            return _data.FirstOrDefault(r => r.Id == id);
        }

        public void Update(Reporter reporter)
        {
            var currentReporter = _data.FirstOrDefault(r => r.Id == reporter.Id);
            if(currentReporter == null)
            {
                return;
            }
            currentReporter.FirstName = reporter.FirstName;
            currentReporter.LastName = reporter.LastName;
            currentReporter.Organization = reporter.Organization;
            currentReporter.Phone = reporter.Phone;
            currentReporter.FirebaseId = reporter.FirebaseId;
            currentReporter.Email = reporter.Email;
           
        }
    }
}
