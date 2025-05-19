using Microsoft.AspNetCore.Mvc;
using QLKS1.API.Repositories.Interfaces;
using System.Data.SqlClient;


namespace QLKS1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardPhongController : ControllerBase
    {
        private readonly IDashboardPhongRepository _dashboardPhongRepository;

        public DashboardPhongController(IDashboardPhongRepository dashboardPhongRepository)
        {
            _dashboardPhongRepository = dashboardPhongRepository;
        }

         [HttpGet]
    public async Task<IActionResult> GetDashboard([FromQuery] DateTime? ngayBatDau, [FromQuery] DateTime? ngayKetThuc)
    {
        var data = await _dashboardPhongRepository.GetDashboardPhongAsync(ngayBatDau, ngayKetThuc);
        return Ok(data);
    }
    }

}

