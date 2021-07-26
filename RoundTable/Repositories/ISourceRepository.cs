using RoundTable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTable.Repositories
{
    public interface ISourceRepository
    {
        List<Source> GetAllSouces(int reporterId);
        Source GetSouceById(int sourceId, int reporterId);
        void AddSource(Source source);
        void UpdateSource(Source source);
        void DeleteSource(int id);

        public void AddSourceToStory(int storyId, int sourceId);

    }
}
