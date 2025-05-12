public class DichVu
{
    public int IDDichVu { get; set; }

    public string TenDichVu { get; set; }

    public decimal GiaDichVu { get; set; }

    public string? MoTa { get; set; }

    public bool Xoa { get; set; }
}

public class DatDichVu
{
    public int IDKhachHang { get; set; }
    public int IDPhong { get; set; }
    public int IDDichVu { get; set; }
    public int SoLan { get; set; }

    public DateTime NgaySuDung { get; set; }
}

public class TinhTienDichVuResponse
{
    public decimal TongTienDichVu { get; set; }
}