using Microsoft.AspNetCore.Mvc;
using QLKS1.API.Repositories.Interfaces;

namespace QLKS1.API.Controllers
{
[Route("api/[controller]")]
[ApiController]
public class DatDichVuController : ControllerBase
{
    private readonly IDatDichVuRepository _repository;

    public DatDichVuController(IDatDichVuRepository repository)
    {
        _repository = repository;
    }

    [HttpPost("dat-dich-vu")]
    public async Task<IActionResult> DatDichVu([FromBody] DatDichVu request)
    {
        var success = await _repository.DatDichVuAsync(request);
        if (!success)
            return BadRequest("Đặt dịch vụ thất bại.");

        return Ok("Đặt dịch vụ thành công.");
    }

    [HttpGet("tinh-tien")]
    public async Task<IActionResult> TinhTien([FromQuery] int idKhachHang, [FromQuery] int idPhong)
    {
        var total = await _repository.TinhTienDichVuAsync(idKhachHang, idPhong);
        return Ok(new { TongTienDichVu = total });
    }
}

}