    using RoundTable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTable.Repositories
{
    public interface IStoryRepository
    {
        List<Story> GetAll(int reporterId);
        Story GetStoryById(int storyId, int reporterId);
        void AddStory(Story story);
        void UpdateStory(Story story);
        void DeleteStory(int id);
        int StoryStatusCount(int statusId, int reportId);
        int StoryCategoryCount(int categoryId, int reporterId);
    }
}
