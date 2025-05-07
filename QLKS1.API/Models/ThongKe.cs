public class ThongKe
{
    public int NgayNhan { get; set; }
    public int NgayTra { get; set; }
    public int? KhachHang { get; set; }   // Dùng nullable vì SUM có thể trả về NULL
    public int PhongTrong { get; set; }
    public int DaDat { get; set; }
}
