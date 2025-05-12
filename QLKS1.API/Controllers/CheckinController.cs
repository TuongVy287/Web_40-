using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QLKS1.API.Repositories.Interfaces;

namespace QLKS1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckinController : ControllerBase
    {
        private readonly ICheckinRepository _checkInRepository;
        public CheckinController(ICheckinRepository checkInRepository)
        {
            _checkInRepository = checkInRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CheckinList>> GetGuestCheckins()
        {
            var guestCheckins = _checkInRepository.GetGuestCheckins();
            return Ok(guestCheckins);
        }
    }
}