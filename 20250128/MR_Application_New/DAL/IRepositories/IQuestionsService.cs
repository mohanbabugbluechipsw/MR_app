using Model_New.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IQuestionsService
    {

        Task<IEnumerable<QuestionsNew>> GetAllAsync();
        Task<QuestionsNew> GetByIdAsync(int id);
        System.Threading.Tasks.Task AddAsync(QuestionsNew question);
        System.Threading.Tasks.Task UpdateAsync(QuestionsNew question);
        Task DeleteAsync(int id);
    }
}
