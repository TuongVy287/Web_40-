public class ThongKeSoLuongNguoiResponse
{
    public int Year { get; set; }
    public int? Month { get; set; } // null nếu là thống kê theo năm
    public int SoNgay { get; set; }
    public int TongSoLuongNguoi { get; set; }
    public int SucChuaToiDa { get; set; }
    public decimal TyLeSuDungPhanTram { get; set; }
}
