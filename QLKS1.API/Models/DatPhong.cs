public class DatPhong
{
    public int IDDatPhong { get; set; }

    public int IDKhachHang { get; set; }

    public int IDPhong { get; set; }

    public DateTime NgayNhan { get; set; }

    public DateTime NgayTra { get; set; }

    public string TrangThaiDatPhong { get; set; }   = "Đang đặt";

    public int SoLuongNguoi { get; set; }

    public string MaKH { get; set; } 

    public string TenPhong { get; set; }
}
