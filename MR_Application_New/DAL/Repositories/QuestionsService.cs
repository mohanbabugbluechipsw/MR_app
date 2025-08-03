using DAL.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model_New.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class QuestionsService : IQuestionsService
    {

        private readonly MrAppDbNewContext _context;

        private readonly ILogger<QuestionsService> _logger;


        public QuestionsService(MrAppDbNewContext context, ILogger<QuestionsService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Tbl_Questions>> GetAllAsync()
        {
            return await _context.tbl_Questions.ToListAsync();
        }

        public async Task<Tbl_Questions> GetByIdAsync(int id)
        {
            return await _context.tbl_Questions.FindAsync(id);
        }

        public async Task AddAsync(Tbl_Questions question)
        {
            try
            {
                _context.tbl_Questions.Add(question);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding question: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateAsync(Tbl_Questions question)
        {
            try
            {
                _context.tbl_Questions.Update(question);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating question: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var question = await _context.tbl_Questions.FindAsync(id);
                if (question != null)
                {
                    _context.tbl_Questions.Remove(question);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting question: {ex.Message}");
                throw;
            }
        }

    }
}
