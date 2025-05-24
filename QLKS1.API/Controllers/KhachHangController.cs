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
    public class KhachHangController : ControllerBase
    {
        private readonly IKhachHangRepository _khachHangRepository;
        public KhachHangController(IKhachHangRepository khachHangRepository)
        {
            _khachHangRepository = khachHangRepository;
        }

        // [HttpGet("all")]
        // public async Task<IActionResult> GetAllKhachHang()
        // {
        //     var khachHangs = await _khachHangRepository.GetAllKhachHangAsync();
        //     return Ok(khachHangs);
        // }

        //GET /api/khachhang/1
        [HttpGet]
        public async Task<IActionResult> GetKhachHangById([FromRoute] int maKH)
        {
            var khachHang = await _khachHangRepository.GetKhachHangByIdAsync(maKH);
            if (khachHang == null)
            {
                return NotFound();
            }
            return Ok(khachHang);
        }

        [HttpGet("ChiTietCheckin")]
        public async Task<IActionResult> GetChiTietKhachHang(int idKhachHang, int idDatPhong)
        {
            var result = await _khachHangRepository.GetChiTietKhachHangAsync(idKhachHang, idDatPhong);
            if (result == null)
            {
                return NotFound(new { Message = "Không tìm thấy thông tin khách hàng hoặc đặt phòng." });
            }

            return Ok(result);
        }
        [HttpGet("guest")]
    public async Task<IActionResult> GetGuestInfo()
    {
        var guests = await _khachHangRepository.GetGuestInfoAsync();
        return Ok(guests);
    }

    [HttpPost("Them")]
    public async Task<IActionResult> ThemKhachHang([FromBody] KhachHang khachHang)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _khachHangRepository.ThemKhachHang(khachHang);
        
        if (result > 0)
        {
            return Ok(new { Message = "Khách hàng đã được thêm thành công!" });
        }

        return StatusCode(500, "Có lỗi xảy ra khi thêm khách hàng.");
    }
    }

     
}