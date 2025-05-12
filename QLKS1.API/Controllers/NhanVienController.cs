using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data;
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

        // GET: api/NhanVien
        [HttpGet]
        public async Task<IActionResult> GetAllNhanVien()
        {
            var nhanViens = await _nhanVienRepository.GetAllNhanVienAsync();
            return Ok(nhanViens);
        }

        // PUT: api/NhanVien/SuaNhanVien
        [HttpPut("SuaNhanVien")]
        public async Task<IActionResult> SuaNhanVien([FromBody] NhanVien nhanVien)
        {
            var result = await _nhanVienRepository.SuaNhanVienAsync(nhanVien);
            if (!result)
                return NotFound(new { message = $"Không tìm thấy nhân viên với mã {nhanVien.MaNV}" });

            return Ok(new { message = "Cập nhật thông tin nhân viên thành công." });
        }

        // DELETE: api/NhanVien/Xoa/{maNV}
        [HttpDelete("Xoa")]
        public async Task<IActionResult> XoaNhanVien([FromBody] NhanVien request)
        {
            if (request == null || string.IsNullOrEmpty(request.MaNV))
            {
                return BadRequest(new { message = "Yêu cầu không hợp lệ." });
            }

            var result = await _nhanVienRepository.XoaNhanVienAsync(request.MaNV);

            if (result)
                return Ok(new { message = "Xóa nhân viên thành công." });
            else
                return NotFound(new { message = "Không tìm thấy nhân viên." });
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.MaNV) ||
                string.IsNullOrWhiteSpace(request.MatKhau) ||
                string.IsNullOrWhiteSpace(request.NewPassword))
            {
                return BadRequest("Thông tin đầu vào không hợp lệ.");
            }

            var success = await _nhanVienRepository.ChangePasswordAsync(
                request.MaNV, request.MatKhau, request.NewPassword
            );

            if (!success)
                return BadRequest("Sai mã nhân viên hoặc mật khẩu.");

            return Ok("Đổi mật khẩu thành công.");
        }

    }
}   
