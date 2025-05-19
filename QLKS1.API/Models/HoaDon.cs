public class HoaDon
{
    public int IDHoaDon { get; set; }
    public int IDKhachHang { get; set; }
    public int IDDatPhong { get; set; }
    public decimal TongTien { get; set; }
    public string TrangThai { get; set; }
    public string PhuongThucThanhToan { get; set; }
    public DateTime NgayTT { get; set; }
}
public class DichVuRequest
{
    public int IDDichVu { get; set; }
    public int SoLuong { get; set; }
}

public class HoaDonRequestt
{
    public int IDKhachHang { get; set; }
    public int IDDatPhong { get; set; }
    public int IDPhong { get; set; }
    public string PhuongThucThanhToan { get; set; } = string.Empty;
    public List<DichVuItem> DichVuList { get; set; } 
}
public class HoaDonResponsee
{
    public int IDHoaDon { get; set; }
    public int IDKhachHang { get; set; }
    public int IDDatPhong { get; set; }
    public decimal TongTien { get; set; }
    public string PhuongThucThanhToan { get; set; } = string.Empty;
}
public class DichVuItem
{
    public int IDDichVu { get; set; }
    public int SoLuong { get; set; }
}


