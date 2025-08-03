using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model_New.Models;
using System.ComponentModel.DataAnnotations;

namespace MR_Application_New.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisplayController : ControllerBase
    {
        private readonly MrAppDbNewContext _locationContext;




        private readonly ILogger<ReviewPlaneController> _logger;

        private readonly IHttpContextAccessor _httpContextAccessor;



        public DisplayController(MrAppDbNewContext mylocationContext, ILogger<ReviewPlaneController> logger , IHttpContextAccessor httpContextAccessor)
        {
            _locationContext = mylocationContext;

            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }




        [HttpPost("save")]

        [Consumes("multipart/form-data")] // ✅ Ensure it accepts FormData
        public async Task<IActionResult> SaveDisplayData([FromForm] DisplayDataDTO data)
        {

            string? rscode = _httpContextAccessor.HttpContext?.Session.GetString("RSCODE");
            string? mrCode = _httpContextAccessor.HttpContext?.Session.GetString("MRCode");
            string? outlet = _httpContextAccessor.HttpContext?.Session.GetString("Outlet");
            string? outlettype = _httpContextAccessor.HttpContext?.Session.GetString("OutletType");


     




            if (data == null)
                return BadRequest("No data received");


            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Validation errors",
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                });
            }



            try
            {
                byte[]? displayPhotoBytes = await ConvertToByteArrayAsync(data.ModelDisplay?.DisplayPhoto);

                byte[]? laundryPhotoBytes = await ConvertToByteArrayAsync(data.SachetHanger?.Laundry?.Photo);
                byte[]? savouryPhotoBytes = await ConvertToByteArrayAsync(data.SachetHanger?.Savoury?.Photo);
                byte[]? hfdPhotoBytes = await ConvertToByteArrayAsync(data.SachetHanger?.Hfd?.Photo);

                var modelDisplay = new ModelDisplay
                {
                    VisibleToShopper = data.ModelDisplay?.VisibleToShopper,
                    DisplayOption = data.ModelDisplay?.DisplayOption,
                    DisplayPhoto = displayPhotoBytes,
                    UnileverSeparate = data.ModelDisplay?.UnileverSeparate,
                    BrandVariantsSeparate = data.ModelDisplay?.BrandVariantsSeparate,
                    NonUnileverNotBetween = data.ModelDisplay?.NonUnileverNotBetween,
                    ShelfStripAvailable = data.ModelDisplay?.ShelfStripAvailable
                };

                var sachetHanger = new SachetHanger
                {
                    SachetHangerAvailable = data.SachetHanger?.SachetHangerAvailable,
                    Laundry = data.SachetHanger?.Laundry != null ? new LaundrySection
                    {
                        Visible = data.SachetHanger.Laundry.Visible,
                        DisplayOption = data.SachetHanger.Laundry.DisplayOption,
                        Photo = laundryPhotoBytes,
                        PlanogramSeparate = data.SachetHanger.Laundry.PlanogramSeparate,
                        BrandVariantsSeparate = data.SachetHanger.Laundry.BrandVariantsSeparate,
                        NonUnileverPlacement = data.SachetHanger.Laundry.NonUnileverPlacement
                    } : null,

                    Savoury = data.SachetHanger?.Savoury != null ? new SavourySection
                    {
                        Visible = data.SachetHanger.Savoury.Visible,
                        DisplayOption = data.SachetHanger.Savoury.DisplayOption,
                        Photo = savouryPhotoBytes,
                        PlanogramSeparate = data.SachetHanger.Savoury.PlanogramSeparate,
                        BrandVariantsSeparate = data.SachetHanger.Savoury.BrandVariantsSeparate,
                        NonUnileverPlacement = data.SachetHanger.Savoury.NonUnileverPlacement
                    } : null,

                    Hfd = data.SachetHanger?.Hfd != null ? new HfdSection
                    {
                        Visible = data.SachetHanger.Hfd.Visible,
                        DisplayOption = data.SachetHanger.Hfd.DisplayOption,
                        Photo = hfdPhotoBytes,
                        PlanogramSeparate = data.SachetHanger.Hfd.PlanogramSeparate,
                        BrandVariantsSeparate = data.SachetHanger.Hfd.BrandVariantsSeparate,
                        NonUnileverPlacement = data.SachetHanger.Hfd.NonUnileverPlacement
                    } : null
                };

                // Save to database
                //_context.ModelDisplays.Add(modelDisplay);
                //_context.SachetHangers.Add(sachetHanger);
                //await _context.SaveChangesAsync();

                return Ok(new { success = true, message = "Data saved successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred", error = ex.Message });
            }





            //_locationContext.ModelDisplays.Add(data.ModelDisplay);
            //_locationContext.SachetHangers.Add(data.SachetHanger);
            //await _context.SaveChangesAsync();

            return Ok(new { message = "Data saved successfully" });
        }





        private async Task<byte[]?> ConvertToByteArrayAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }


    }









    public class DisplayDataDTO
    {
        public ModelDisplayDTO ModelDisplay { get; set; }
        public SachetHangerDTO SachetHanger { get; set; }
    }

    public class ModelDisplayDTO
    {
        public string? VisibleToShopper { get; set; }
        public string? DisplayOption { get; set; }
        public IFormFile? DisplayPhoto { get; set; } // ✅ Accepts File Upload
        public string? UnileverSeparate { get; set; }
        public string? BrandVariantsSeparate { get; set; }
        public string? NonUnileverNotBetween { get; set; }
        public string? ShelfStripAvailable { get; set; }
    }

    public class SachetHangerDTO
    {
        public string? SachetHangerAvailable { get; set; }
        public LaundrySectionDTO? Laundry { get; set; }
        public SavourySectionDTO? Savoury { get; set; }
        public HfdSectionDTO? Hfd { get; set; }
    }

    public class LaundrySectionDTO
    {
        public string? Visible { get; set; }
        public string? DisplayOption { get; set; }
        public IFormFile? Photo { get; set; } // ✅ Accepts File Upload
        public string? PlanogramSeparate { get; set; }
        public string? BrandVariantsSeparate { get; set; }
        public string? NonUnileverPlacement { get; set; }
    }

    public class SavourySectionDTO
    {
        public string? Visible { get; set; }
        public string? DisplayOption { get; set; }
        public IFormFile? Photo { get; set; } // ✅ Accepts File Upload
        public string? PlanogramSeparate { get; set; }
        public string? BrandVariantsSeparate { get; set; }
        public string? NonUnileverPlacement { get; set; }
    }

    public class HfdSectionDTO
    {
        public string? Visible { get; set; }
        public string? DisplayOption { get; set; }
        public IFormFile? Photo { get; set; } // ✅ Accepts File Upload
        public string? PlanogramSeparate { get; set; }
        public string? BrandVariantsSeparate { get; set; }
        public string? NonUnileverPlacement { get; set; }
    }





    public class ModelDisplay
    {
        [Key]
        public int Id { get; set; }
        public string? VisibleToShopper { get; set; }
        public string? DisplayOption { get; set; }
        public byte[]? DisplayPhoto { get; set; } // Store as byte[]
        public string? UnileverSeparate { get; set; }
        public string? BrandVariantsSeparate { get; set; }
        public string? NonUnileverNotBetween { get; set; }
        public string? ShelfStripAvailable { get; set; }
    }






    public class SachetHanger
    {
        [Key]
        public int Id { get; set; }
        public string? SachetHangerAvailable { get; set; }

        public LaundrySection? Laundry { get; set; }
        public SavourySection? Savoury { get; set; }
        public HfdSection? Hfd { get; set; }
    }

    public class LaundrySection
    {
        [Key]
        public int Id { get; set; }
        public string? Visible { get; set; }
        public string? DisplayOption { get; set; }
        public byte[]? Photo { get; set; } // Store image as byte array
        public string? PlanogramSeparate { get; set; }
        public string? BrandVariantsSeparate { get; set; }
        public string? NonUnileverPlacement { get; set; }
    }

    public class SavourySection
    {
        [Key]
        public int Id { get; set; }
        public string? Visible { get; set; }
        public string? DisplayOption { get; set; }
        public byte[]? Photo { get; set; }
        public string? PlanogramSeparate { get; set; }
        public string? BrandVariantsSeparate { get; set; }
        public string? NonUnileverPlacement { get; set; }
    }

    public class HfdSection
    {
        [Key]
        public int Id { get; set; }
        public string? Visible { get; set; }
        public string? DisplayOption { get; set; }
        public byte[]? Photo { get; set; }
        public string? PlanogramSeparate { get; set; }
        public string? BrandVariantsSeparate { get; set; }
        public string? NonUnileverPlacement { get; set; }
    }


}
