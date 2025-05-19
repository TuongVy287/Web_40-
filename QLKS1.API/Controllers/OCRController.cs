// using Microsoft.AspNetCore.Mvc;
// using QLKS.API.Services;


// namespace QLKS1.API.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class OCRController : ControllerBase
//     {
//         private readonly IOcrService _ocrService;

//         public OCRController(OcrService ocrService)
//         {
//             _ocrService = ocrService;
//         }

//         [HttpPost("ExtractCCCD")]
//         public async Task<IActionResult> ExtractCCCD([FromForm] IFormFile file)
//         {
//             if (file == null || file.Length == 0)
//                 return BadRequest("No file uploaded.");

//             using var stream = file.OpenReadStream();
//             var result = _ocrService.ExtractCCCDInfo(stream);

//             return Ok(result);
//         }
//     }
// }
