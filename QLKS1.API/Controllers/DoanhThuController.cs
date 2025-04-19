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
    public class DoanhThuController : ControllerBase
    {
        private readonly IDoanhThuRepository _doanhthuRepository;
        public DoanhThuController(IDoanhThuRepository doanhthuRepository)
        {
            _doanhthuRepository = doanhthuRepository;
        }

        //GET /api/khachhang/1
        [HttpGet("ByDate")]
        public async Task<IActionResult> GetDoanhThuByDateAsync([FromQuery] DateTime date)
        {
            if (date < new DateTime(1753, 1, 1))
            {
                return BadRequest("Ngày không hợp lệ. Ngày phải lớn hơn hoặc bằng 01/01/1753.");
            }

            var doanhthu = await _doanhthuRepository.GetDoanhThuByDayAsync(date);
            if (doanhthu == null)
            {
                return NotFound();
            }

            return Ok(doanhthu);
        }
    //     [HttpGet("ByRange")]
    //     public async Task<IActionResult> GetDoanhThuByRangeAsync(
    // [FromQuery] DateTime? fromDate,
    // [FromQuery] DateTime? toDate)
    //     {
    //         var start = fromDate ?? new DateTime(1753, 1, 1);
    //         var end = toDate ?? DateTime.Now;

    //         if (start > end)
    //         {
    //             return BadRequest("Ngày bắt đầu không được lớn hơn ngày kết thúc.");
    //         }

    //         var doanhthu = await _doanhthuRepository.GetDoanhThuTheoNgayAsync(start, end);
    //         return Ok(doanhthu);
    //     }


    }
}