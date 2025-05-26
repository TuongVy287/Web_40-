using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ThongTinDealController : ControllerBase
{
    private readonly IThongTinDealRepository _thongTinDealRepository;

    public ThongTinDealController(IThongTinDealRepository thongTinDealRepository)
    {
        _thongTinDealRepository = thongTinDealRepository;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllThongTinDeal()
    {
        var deals = await _thongTinDealRepository.GetAllThongTinDealAsync();
        return Ok(deals);
    }
}