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
    }
}