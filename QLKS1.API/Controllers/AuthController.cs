using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dapper;

namespace QLKS1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration; private readonly IDbConnection _db;
        public AuthController(IConfiguration configuration, IDbConnection db)
        {
            _configuration = configuration;
            _db = db;
        }

        [HttpPost("DangKy")]
        public async Task<IActionResult> Register(UserRegisterDTO request)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.MatKhau);

            var parameters = new DynamicParameters();
            parameters.Add("@HoTen", request.HoTen);
            parameters.Add("@MatKhau", hashedPassword);
            parameters.Add("@GioiTinh", request.GioiTinh);
            parameters.Add("@ChucDanh", request.ChucDanh);
            parameters.Add("@SoDienThoai", request.SoDienThoai);
            parameters.Add("@Email", request.Email);
            parameters.Add("@CaLamViec", request.CaLamViec);
            parameters.Add("@Luong", request.Luong);

            var result = await _db.ExecuteAsync("spAPI_NhanVien_Them", parameters, commandType: CommandType.StoredProcedure);

            if (result > 0)
            {
                return Ok("User registered successfully.");
            }

            return BadRequest("Registration failed.");
        }

        // [HttpGet("login")]
        // public async Task<IActionResult> Login([FromQuery] string MaNV, [FromQuery] string MatKhau)
        // {
        //     var hashedPassword = BCrypt.Net.BCrypt.HashPassword(MatKhau);
        //     var parameters = new DynamicParameters();
        //     parameters.Add("@MaNV", MaNV);
        //     parameters.Add("@MatKhau", hashedPassword);

        //     var user = await _db.QueryFirstOrDefaultAsync<User>(
        //         "spAPI_NhanVien_Login",
        //         parameters,
        //         commandType: CommandType.StoredProcedure);

        //     if (user == null)
        //     {
        //         return Unauthorized("Invalid credentials");
        //     }

        //     string token = CreateToken(user);
        //     return Ok(new { user, token });
        // }
        [HttpPost("login")]
        public async Task<IActionResult> Login(NhanVienDTO request)
        {
            // var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.MatKhau);

            var parameters = new DynamicParameters();
            parameters.Add("@MaNV", request.MaNV);

            // Lấy thông tin user từ cơ sở dữ liệu
            var user = await _db.QueryFirstOrDefaultAsync<NhanVien>(
                "spAPI_NhanVien_Login",
                parameters,
                commandType: CommandType.StoredProcedure);

            // Kiểm tra nếu user không tồn tại
            // if (user == null)
            // {
            //     return Unauthorized("User không tồn tại");
            // }

            // So sánh mật khẩu nhập vào với mật khẩu đã hash trong cơ sở dữ liệu
            if (!BCrypt.Net.BCrypt.Verify(request.MatKhau, user.MatKhau))
            {
                // return Unauthorized("Sai Mật Khẩu");
                return Ok(new { message = "Sai Mật Khẩu", success = false, MaNV = request.MaNV, password = request.MatKhau });
            }

            // Tạo token nếu đăng nhập thành công
            // string token = CreateToken(user);
            return Ok(new { user });
        }


        private string CreateToken(NhanVien user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.MaNV),
                new Claim(ClaimTypes.Role, user.ChucDanh)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}