using RoundTable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTable.Repositories
{
    public interface IReporterRepository
    {
        public Reporter GetById(int id);
        public Reporter GetByFirebaseUserId(string firebaseUserId);
        public void Add(Reporter reporter);
    }
}
