using Microsoft.AspNetCore.Mvc;
using QLKS1.API.Repositories.Interfaces;
using System.Data.SqlClient;


namespace QLKS1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatPhongController : ControllerBase
    {
        private readonly IDatPhongRepository _datPhongRepository;

        public DatPhongController(IDatPhongRepository datPhongRepository)
        {
            _datPhongRepository = datPhongRepository;
        }

        // GET: api/DichVu
        [HttpGet]
        public async Task<IActionResult> GetAllDatPhong()
        {
            var dichvus = await _datPhongRepository.GetAllDatPhong();
            return Ok(dichvus);
        }

        // GET: api/DichVu/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDatPhongByIdAsync(int id)
        {
            var dichvu = await _datPhongRepository.GetDatPhongByIdAsync(id);
            if (dichvu == null)
                return NotFound();

            return Ok(dichvu);
        }

        [HttpPost("DatPhong")]
        public async Task<IActionResult> BookRoom([FromBody] DatPhongRequest request)
        {
            try
            {
                var result = await _datPhongRepository.CreateBookingAsync(request);
                if (result == null)
                    return BadRequest("Đặt phòng thất bại.");

                return Ok(result);
            }
            catch (SqlException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("cancel/{id}")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var result = await _datPhongRepository.CancelBookingAsync(id);
            if (result == null)
                return NotFound("Không tìm thấy đặt phòng.");
            return Ok(result);
        }
        [HttpGet("theo-thang")]
    public async Task<IActionResult> GetDatPhongTheoThang([FromQuery] string thangChu)
    {
        if (string.IsNullOrWhiteSpace(thangChu))
        {
            return BadRequest("Tham số tháng không được để trống.");
        }

        try
        {
            var data = await _datPhongRepository.GetDatPhongTheoThangAsync(thangChu);
            return Ok(data);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Lỗi server: {ex.Message}");
        }
    }
    }

}

