using DAL.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Model_New.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MR_Application_New.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly IQuestionsService _questionsService;
        private readonly ILogger<QuestionsController> _logger;
        private readonly MrAppDbNewContext _context;

        public QuestionsController(IQuestionsService questionsService, MrAppDbNewContext context)
        {
            _questionsService = questionsService;
            _context = context;  // Initialize the context
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var questions = await _questionsService.GetAllAsync();
            return Ok(questions);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var question = await _questionsService.GetByIdAsync(id);
            if (question == null) return NotFound();
            return Ok(question);
        }

        // GET: Questions/Create








        [HttpGet]
        public IActionResult Create()






        {
            // Fetch distributors from the Distributor table
            var distributors = _context.TblDistributors.Select(d => new
            {

                d.DistributorName,
                d.Distributor
            }).ToList();  // Fetch distributors from the database

            // Pass distributors to the view using ViewBag
            ViewBag.Distributors = new SelectList(distributors, "Distributor", "DistributorName");

            return View();
        }









        [HttpGet]
        public IActionResult GetPartyData(int distributorId)
        {
            // Fetch all party data based on the selected distributor
            var partyData = _context.OutLetMasterDetails
                                     .Where(p => p.Rscode == distributorId)
                                     .Select(p => new
                                     {
                                         p.PartyHllcode
                                     })
                                     .ToList();  // Use ToList() instead of FirstOrDefault()

            if (partyData == null || partyData.Count == 0)  // Check if the list is empty
            {
                return NotFound("No party data found for the selected distributor.");
            }

            return Ok(partyData);  // Return the list of party data
        }



        [HttpGet]
        public IActionResult GetPartyDetailsByHllcode(string partyHllcode)
        {
            var partyDetails = _context.OutLetMasterDetails
                                       .Where(p => p.PartyHllcode == partyHllcode)
                                       .Select(p => new { p.PartyName, p.PartyMasterCode })
                                       .FirstOrDefault();

            if (partyDetails == null)
            {
                return NotFound("Party details not found for the selected HLL Code.");
            }

            return Json(partyDetails);
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var questions = await _questionsService.GetAllAsync();
            return View(questions);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string Question, string Distributor, string PartyHllcode, string PartyName, string PartyMasterCode, string Type, bool Status, bool Ba)
        {



            var question = new QuestionsNew
            {
                Question = Question,
                Distributor = Distributor,
                PartyHllcode = PartyHllcode,
                PartyName = PartyName,
                PartyMasterCode = PartyMasterCode,
                Type = Type,
                Status = Status,
                Ba = Ba,
                CreatedAt = DateTime.Now


            };
            if (ModelState.IsValid)
            {
                await _questionsService.AddAsync(question);
                return RedirectToAction("Index", "Admin");
            }

            // Repopulate the distributor dropdown in case of errors
            var distributors = _context.TblDistributors.ToList();
            ViewBag.Distributors = new SelectList(distributors, "Distributor", "DistributorName");

            return View(question);


        }





        // POST: Questions/Create
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] QuestionsNew question)
        {
            if (id != question.QuestionId) return BadRequest("ID mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _questionsService.UpdateAsync(question);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating question");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _questionsService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting question");
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
