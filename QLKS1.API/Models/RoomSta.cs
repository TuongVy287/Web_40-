public class PhongTrangThai
{
    public string TrangThaiPhong { get; set; }
    public int SoLuong { get; set; }
}

public class PhongLoai
{
    public string LoaiPhong { get; set; }
    public int SoLuong { get; set; }
}

public class PhongThongKe
{
    public IEnumerable<PhongTrangThai> TrangThai { get; set; }
    public IEnumerable<PhongLoai> DaDat { get; set; }
    public IEnumerable<PhongLoai> Trong { get; set; }
}
