
public class NhanVien
{
    public int MaNV { get; set; }           // Mã nhân viên
    public string TenNV { get; set; }          // Tên nhân viên
    public string GioiTinh { get; set; }       // Giới tính
    public DateTime NgaySinh { get; set; }     // Ngày sinh
    public string SDT { get; set; }            // Số điện thoại
    public string CCCD { get; set; }           // Căn cước công dân
    public string DiaChi { get; set; }         // Địa chỉ
    public string MaCV { get; set; }           // Mã chức vụ (foreign key)
}
