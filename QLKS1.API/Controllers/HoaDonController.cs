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
    public class HoaDonController : ControllerBase
    {
        private readonly IHoaDonRepository _hoaDonRepository;
        public HoaDonController(IHoaDonRepository hoaDonRepository)
        {
            _hoaDonRepository = hoaDonRepository;
        }

        //GET /api/khachhang/1
        [HttpGet]
        public async Task<IActionResult> GetHoaDonByIdAsync([FromRoute] int IDHoaDon)
        {
            var khachHang = await _hoaDonRepository.GetHoaDonByIdAsync(IDHoaDon);
            if (khachHang == null)
            {
                return NotFound();
            }
            return Ok(khachHang);
        }

        [HttpPost("them")]
    public async Task<IActionResult> ThemHoaDon([FromBody] HoaDonRequestt request)
    {
        try
        {
            var result = await _hoaDonRepository.ThemHoaDonAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    }
}