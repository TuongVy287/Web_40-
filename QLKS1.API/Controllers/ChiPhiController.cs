using Microsoft.AspNetCore.Mvc;
using QLKS1.API.Repositories.Interfaces;

namespace QLKS1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiPhiController : ControllerBase
    {
        private readonly IChiPhiRepository _chiPhiRepository;

        public ChiPhiController(IChiPhiRepository chiPhiRepository)
        {
            _chiPhiRepository = chiPhiRepository;
        }

        // GET: api/ChiPhi
        [HttpGet]
        public async Task<IActionResult> GetAllChiPhi()
        {
            var chiPhis = await _chiPhiRepository.GetAllChiPhi();
            return Ok(chiPhis);
        }

        // GET: api/ChiPhi/5
        [HttpGet("{ngay}")]
        public async Task<IActionResult> GetChiPhiByDateAsync(DateTime ngay)
        {
            var chiPhi = await _chiPhiRepository.GetChiPhiByDateAsync(ngay);
            if (chiPhi == null)
                return NotFound();

            return Ok(chiPhi);
        }
    }
}
