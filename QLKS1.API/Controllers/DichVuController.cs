using Microsoft.AspNetCore.Mvc;
using QLKS1.API.Repositories.Interfaces;

namespace QLKS1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuController : ControllerBase
    {
        private readonly IDichVuRepository _dichVuRepository;

        public DichVuController(IDichVuRepository dichVuRepository)
        {
            _dichVuRepository = dichVuRepository;
        }

        // GET: api/DichVu
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var dichvus = await _dichVuRepository.GetAllDichVu();
            return Ok(dichvus);
        }

        // GET: api/DichVu/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var dichvu = await _dichVuRepository.GetDichVuByIdAsync(id);
            if (dichvu == null)
                return NotFound();

            return Ok(dichvu);
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
