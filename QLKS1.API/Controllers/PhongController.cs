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
    public class PhongController : ControllerBase
    {
        private readonly IPhongRepository _phongRepository;
        public PhongController(IPhongRepository phongRepository)
        {
            _phongRepository = phongRepository;
        }

        //GET /api/khachhang/1
        [HttpGet]
        public async Task<IActionResult> GetPhongByIdAsync([FromRoute] int IDPhong)
        {
            var phong = await _phongRepository.GetPhongByIdAsync(IDPhong);
            if (phong == null)
            {
                return NotFound();
            }
            return Ok(phong);
        }
    }
}