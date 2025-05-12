public class CheckinList
{
    public int IDKhachHang { get; set; }
    public int IDDatPhong { get; set; }
    public int IDPhong { get; set; }
    public string HoTen { get; set; } = string.Empty;

    public string MaKhachHang { get; set; } = string.Empty;

    public string TenPhong {get;set;}
    public DateTime NgayNhan { get; set; }
    public DateTime NgayTra { get; set; }

    public string TrangThaiDatPhong {get;set;}
}
