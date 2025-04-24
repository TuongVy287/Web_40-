// DTOs/UserRegisterDTO.cs
public class NhanVienDTO
{
    public int IDNhanVien { get; set; }
    public string MaNV { get; set; } = string.Empty;
    public string HoTen { get; set; } = string.Empty;
    public int GioiTinh { get; set; } 
    public string ChucDanh { get; set; } = string.Empty;
    public string SoDienThoai { get; set; }
    public string Email { get; set; } = string.Empty;
    public string CaLamViec { get; set; } = string.Empty;
    public string NgayTao { get; set; } = string.Empty;
    public int Luong { get; set; } 
    public string MatKhau { get; set; } = string.Empty;
    public int Xoa { get; set; }

}