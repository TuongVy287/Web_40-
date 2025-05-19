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
    public class ThongKeController : ControllerBase
    {
        private readonly IThongKeService _repo;

        public ThongKeController(IThongKeService repo)
        {
            _repo = repo;
        }

        [HttpGet("homnay")]
        public async Task<IActionResult> GetThongKe()
        {
            var result = await _repo.GetThongKeNgayHomNayAsync();
            return Ok(result);
        }
        [HttpGet("SoLuongNguoiTheoThang")]
    public async Task<IActionResult> GetThongKeSoLuongNguoi([FromQuery] string nam)
    {
        if (string.IsNullOrWhiteSpace(nam))
            return BadRequest("Vui lòng nhập giá trị cho năm, ví dụ: 'Tất cả' hoặc '2025'");

        var data = await _repo.GetSoLuongNguoiTheoThangHoacNamAsync(nam);
        return Ok(data);
    }
    }

}
