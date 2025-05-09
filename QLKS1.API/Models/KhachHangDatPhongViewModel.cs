public class KhachHangDatPhongViewModel
{
    public int MaKH { get; set; }
    public string TenKH { get; set; }
    public string GioiTinh { get; set; }
    public string SDT { get; set; }
    public string CMND { get; set; }
    public string DiaChi { get; set; }

    public int IDDatPhong { get; set; }

    public int IDKhachHang { get; set; }

    public int IDPhong { get; set; }

    public DateTime NgayNhan { get; set; }

    public DateTime NgayTra { get; set; }

    public string TrangThaiDatPhong { get; set; }   = "Đang đặt";

    public int SoLuongNguoi { get; set; }
}
