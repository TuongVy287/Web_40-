public class NhanVien
{
    public int MaNhanVien { get; set; }
    public string HoTen { get; set; } = string.Empty;
    public bool GioiTinh { get; set; }
    public string ChucDanh { get; set; } = string.Empty;
    public string SoDienThoai { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string CaLamViec { get; set; } = string.Empty;
    public string QuyenTruyCap { get; set; } = string.Empty;
    public DateTime NgayTao { get; set; }
    public int Luong { get; set; }
    public string TaiKhoan { get; set; } = string.Empty;
    public string MatKhau { get; set; } = string.Empty;
    public bool Xoa { get; set; }
}
