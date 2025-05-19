using Microsoft.AspNetCore.Mvc;
using QLKS1.API.Repositories.Interfaces;
using System.Data.SqlClient;


namespace QLKS1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimPhongController : ControllerBase
    {
        private readonly ITimPhongPhuHopRepository _PhongphuhopRepository;

        public TimPhongController(ITimPhongPhuHopRepository PhongphuhopRepository)
        {
            _PhongphuhopRepository = PhongphuhopRepository;
        }

         [HttpGet("phong-phu-hop")]
    public async Task<IActionResult> GetPhongPhuHop([FromQuery] DateTime checkIn, [FromQuery] DateTime checkOut, [FromQuery] int adults, [FromQuery] int children)
    {
        try
        {
            var phongPhuHop = await _PhongphuhopRepository.TimPhongAsync(checkIn, checkOut, adults, children);
            return Ok(phongPhuHop);
        }
        catch (SqlException ex)
        {
            return BadRequest(ex.Message); // nếu stored procedure THROW lỗi
        }
    }
    }

}

