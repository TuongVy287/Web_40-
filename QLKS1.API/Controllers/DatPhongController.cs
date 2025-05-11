using Microsoft.AspNetCore.Mvc;
using QLKS1.API.Repositories.Interfaces;

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
        public async Task<IActionResult> ThemDatPhong([FromBody] DatPhongRequest request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "Yêu cầu không hợp lệ." });
            }

            try
            {
                var datPhong = await _datPhongRepository.CreateBookingAsync(request);
                return Ok(datPhong);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Có lỗi xảy ra trong quá trình xử lý." });
            }
        }
    }
    // [HttpPost]
    // public async Task<IActionResult> DatPhongMoi([FromBody] DatPhong datPhong)
    // {
    //     if (!ModelState.IsValid)
    //         return BadRequest(ModelState);

    //     var id = await _datPhongRepository.InsertDatPhongAsync(datPhong);
    //     return Ok(new { Message = "Đặt phòng thành công", ID = id });
    // }



    // POST: api/DichVu
    // [HttpPost]
    // public async Task<IActionResult> CreateAsync([FromBody] CreateDichVuDto newDichVu)
    // {
    //     var dichvu = await _dichVuRepository.CreateAsync(newDichVu);
    //     return CreatedAtAction(nameof(GetByIdAsync), new { id = dichvu.IDDichVu }, dichvu);
    // }

    // // PUT: api/DichVu/5
    // [HttpPut("{id}")]
    // public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateDichVuDto updateDto)
    // {
    //     var updated = await _dichVuRepository.UpdateAsync(id, updateDto);
    //     if (!updated)
    //         return NotFound();

    //     return NoContent();
    // }

    // // DELETE: api/DichVu/5
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteAsync(int id)
    // {
    //     var deleted = await _dichVuRepository.DeleteAsync(id);
    //     if (!deleted)
    //         return NotFound();

    //     return NoContent();
    // }
}

