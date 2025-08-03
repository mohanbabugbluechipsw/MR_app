using DAL.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model_New.Models;

namespace MR_Application_New.Controllers
{
    public class LocationsController : Controller
    {

        private readonly MrAppDbNewContext _locationContext;
        private readonly IGenericRepository<OutLetMasterDetail, MrAppDbNewContext> _mapService;


        //private List<Location> GetDestinations()
        //{
        //    return _locationContext.OutLetMasterDetails.ToList(); // Retrieve all locations from the database
        //}



        private List<OutLetMasterDetail> GetDestinations()
        {
            return _locationContext.OutLetMasterDetails.ToList(); // Retrieve all locations from the database
        }
        public LocationsController(MrAppDbNewContext mylocationContext, IGenericRepository<OutLetMasterDetail, MrAppDbNewContext> mapService)
        {
            _locationContext = mylocationContext;
            _mapService = mapService;
        }



        public IActionResult Index()
        {
            var locations = _locationContext.OutLetMasterDetails.ToList();

            return View(locations);

        }

        //public IActionResult Getcurrent()
        //{
        //    // Fetch destinations from the database to show in the dropdown
        //    var destinations = GetDestinations().GroupBy(d=>d.Rscode).Select(g=>g.First()).ToList();  // Replace with actual database retrieval logic




        //    return View(destinations);
        //}

        //[HttpGet]
        //public JsonResult GetPartyHLLCodesByRscode(int rscode)
        //{
        //    var partyHLLCodes = _locationContext.OutLetMasterDetails
        //                        .Where(l => l.Rscode == rscode)
        //                        .Select(l => l.PartyHllcode)
        //                        .Distinct() // Ensure unique PartyHLLCodes
        //                        .ToList();

        //    return Json(partyHLLCodes);
        //}
        public IActionResult Getcurrent(int? rscode)
        {
            if (rscode.HasValue) // Check if RSCODE is provided in the AJAX request
            {
                // Process the AJAX request to get unique PartyHLLCodes for the selected RSCODE
                var partyNames = _locationContext.OutLetMasterDetails
                                          .AsNoTracking()
                                    .Where(l => l.Rscode == rscode.Value)
                                    .Select(l => l.PartyName)
                                    .Distinct()
                                    .ToList();

                return Json(partyNames); // Return the data as JSON for AJAX request
            }

            // If no RSCODE, render the view with unique RSCODE destinations
            var destinations = GetDestinations()
                                .GroupBy(d => d.Rscode)
                                .Select(g => g.First())
                                .ToList();

            return View(destinations); // Return the view for non-AJAX requests
        }


        [HttpGet]
        public JsonResult GetLocationDetails(int rscode, string partyName)
        {
            var locationDetails = _locationContext.OutLetMasterDetails
                                .Where(l => l.Rscode == rscode && l.PartyName == partyName)
                                .Select(l => new
                                {

                                    l.Address1,
                                    l.Address2,
                                    l.Address3,
                                    l.Address4,
                                    l.PartyHllcode,
                                    l.Latitude,
                                    l.Longitude

                                })
                                .FirstOrDefault();

            return Json(locationDetails);
        }




        //[HttpGet("getCoordinates")]
        //public async Task<IActionResult> GetCoordinates(string Address1, string Address2, string Address3, string Address4, int? id)
        //{
        //    // Log received parameters
        //    Console.WriteLine($"Received Address1: {Address1}, Address2: {Address2}, Address3: {Address3}, Address4: {Address4}, id: {id}");

        //    // Check if any required parameter is provided
        //    if (string.IsNullOrEmpty(Address1) && string.IsNullOrEmpty(Address2) && string.IsNullOrEmpty(Address3) && string.IsNullOrEmpty(Address4) && id == null)
        //    {
        //        return BadRequest("At least one location parameter or ID must be provided.");
        //    }

        //    // Fetch coordinates from the service
        //    var coordinates = await _mapService.GetCoordinatesAsync(Address1, Address2, Address3, Address4, id);

        //    // Log what is returned from the service
        //    Console.WriteLine($"Coordinates found: {coordinates}");

        //    if (coordinates == null)
        //    {
        //        return NotFound("Coordinates not found.");
        //    }

        //    return Ok(coordinates);
        //}



        //[HttpGet]


        //public async Task<IActionResult> GetCoordinatesAsync(int  rscode, string partyHLLCode)
        //{
        //    // Replace this with your logic to fetch coordinates based on Rscode and PartyHLLCode


        //    // Fetch coordinates from the service
        //    var coordinates = await _mapService.GetCoordinatesAsync(rscode,partyHLLCode);

        //    // Log what is returned from the service
        //    Console.WriteLine($"Coordinates found: {coordinates}");

        //    if (coordinates == null)
        //    {
        //        return NotFound("Coordinates not found.");
        //    }

        //    return Ok(coordinates);

        //}




        [HttpGet]
        public IActionResult Create()
        {
            var destinations = _locationContext.OutLetMasterDetails.Select(l => new
            {
                l.Rscode,
                l.PartyHllcode,
            }).ToList();

            ViewBag.Destinations = destinations;


            ViewBag.SourceLatitude = null;
            ViewBag.SourceLongitude = null;
            ViewBag.DestinationLatitude = null;
            ViewBag.DestinationLongitude = null;

            return View();
        }


    }
}
