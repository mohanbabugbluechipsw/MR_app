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

        Task<IEnumerable<Tbl_Questions>> GetAllAsync();
        Task<Tbl_Questions> GetByIdAsync(int id);
        System.Threading.Tasks.Task AddAsync(Tbl_Questions question);
        System.Threading.Tasks.Task UpdateAsync(Tbl_Questions question);
        Task DeleteAsync(int id);
    }
}
