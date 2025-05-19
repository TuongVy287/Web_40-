using Microsoft.AspNetCore.Mvc;
using QLKS1.API.Repositories.Interfaces;

namespace QLKS1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietHoaDonController : ControllerBase
    {
        private readonly IChiTietHoaDonRepository _chitiethoaDonRepository;

        public ChiTietHoaDonController(IChiTietHoaDonRepository chitiethoaDonRepository)
        {
            _chitiethoaDonRepository = chitiethoaDonRepository;
        }

        // GET: api/DichVu
        [HttpGet]
        public async Task<IActionResult> GetAllChiTietHoaDon()
        {
            var dichvus = await _chitiethoaDonRepository.GetAllChiTietHoaDon();
            return Ok(dichvus);
        }

        // GET: api/DichVu/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetChiTietHoaDonByIdAsync(int id)
        {
            var dichvu = await _chitiethoaDonRepository.GetChiTietHoaDonByIdAsync(id);
            if (dichvu == null)
                return NotFound();

            return Ok(dichvu);
        }

        [HttpPost("them")]
    public async Task<IActionResult> ThemChiTietHoaDon(int idHoaDon, int idDichVu, int soLuong)
    {
        try
        {
            var tongTien = await _chitiethoaDonRepository.ThemChiTietHoaDonAsync(idHoaDon, idDichVu, soLuong);
            return Ok(new { TongTien = tongTien });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

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
}
