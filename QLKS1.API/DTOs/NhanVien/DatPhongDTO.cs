public class DatPhongDTO
{
    public int IDKhachHang { get; set; }
    public int IDPhong { get; set; }
    public DateTime NgayNhan { get; set; }
    public DateTime NgayTra { get; set; }
    public string TrangThaiDatPhong { get; set; } = "Đang đặt"; // mặc định
    public int SoLuongNguoi { get; set; }
}
