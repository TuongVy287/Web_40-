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
    public class NhanVienController : ControllerBase
    {
        private readonly INhanVienRepository _nhanVienRepository;
        public NhanVienController(INhanVienRepository nhanVienRepository)
        {
            _nhanVienRepository = nhanVienRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllNhanVien()
        {
            var nhanViens = await _nhanVienRepository.GetAllNhanVienAsync();
            return Ok(nhanViens);
        }
    }
    
}