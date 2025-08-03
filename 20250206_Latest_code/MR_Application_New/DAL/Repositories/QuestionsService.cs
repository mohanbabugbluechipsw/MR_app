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

        public async Task<IEnumerable<QuestionsNew>> GetAllAsync()
        {
            return await _context.QuestionsNews.ToListAsync();
        }

        public async Task<QuestionsNew> GetByIdAsync(int id)
        {
            return await _context.QuestionsNews.FindAsync(id);
        }

        public async Task AddAsync(QuestionsNew question)
        {
            try
            {
                _context.QuestionsNews.Add(question);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding question: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateAsync(QuestionsNew question)
        {
            try
            {
                _context.QuestionsNews.Update(question);
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
                var question = await _context.QuestionsNews.FindAsync(id);
                if (question != null)
                {
                    _context.QuestionsNews.Remove(question);
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
