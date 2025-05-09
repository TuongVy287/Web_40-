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
    public class NhanVienController : ControllerBase
    {
        private readonly INhanVienRepository _nhanVienRepository;
        public NhanVienController(INhanVienRepository nhanVienRepository)
        {
            _nhanVienRepository = nhanVienRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllNhanVien()
        {
            var nhanViens = await _nhanVienRepository.GetAllNhanVienAsync();
            return Ok(nhanViens);
        }

        // [HttpPut("{maNv}")]
        // public async Task<IActionResult> SuaNhanVien(string maNv, [FromBody] NhanVien nhanVien)
        // {
        //     // Bỏ kiểm tra, dùng trực tiếp body
        //     var result = await _nhanVienRepository.SuaNhanVienAsync(nhanVien);
        //     if (!result)
        //         return NotFound($"Không tìm thấy nhân viên với mã {nhanVien.MaNv}");

        //     return Ok("Cập nhật thông tin nhân viên thành công.");
        // }

        [HttpPut]
        public async Task<IActionResult> SuaNhanVien([FromBody] NhanVien nhanVien)
        {
            var result = await _nhanVienRepository.SuaNhanVienAsync(nhanVien);
            if (!result)
                return NotFound($"Không tìm thấy nhân viên với mã {nhanVien.MaNV}");

            return Ok("Cập nhật thông tin nhân viên thành công.");
        }


    }

}