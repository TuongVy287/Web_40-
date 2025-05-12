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
    public class PhongController : ControllerBase
    {
        private readonly IPhongRepository _phongRepository;
        public PhongController(IPhongRepository phongRepository)
        {
            _phongRepository = phongRepository;
        }

        //GET /api/khachhang/1
        [HttpGet]
        public async Task<IActionResult> GetPhongByIdAsync([FromRoute] int IDPhong)
        {
            var phong = await _phongRepository.GetPhongByIdAsync(IDPhong);
            if (phong == null)
            {
                return NotFound();
            }
            return Ok(phong);
        }
        // GET /api/phong/trangthai/Trống
        [HttpGet("trangthai/{trangThai}")]
        public async Task<IActionResult> GetPhongsByTrangThaiAsync([FromRoute] string trangThai)
        {
            var phongs = await _phongRepository.GetPhongsByTrangThaiAsync(trangThai);
            if (!phongs.Any())
            {
                return NotFound("Không có phòng với trạng thái này.");
            }
            return Ok(phongs);
        }
         [HttpGet("thongke")]
    public async Task<IActionResult> GetThongKePhong()
    {
        var result = await _phongRepository.GetThongKePhongAsync();
        return Ok(result);
    }
     [HttpGet("theo-trang-thai")]
    public async Task<IActionResult> GetPhongTheoTrangThai([FromQuery] string trangThai)
    {
        if (string.IsNullOrWhiteSpace(trangThai))
        {
            return BadRequest("Vui lòng nhập trạng thái phòng.");
        }

        var result = await _phongRepository.GetPhongTheoTrangThaiAsync(trangThai);
        return Ok(result);
    }

    }
    
}