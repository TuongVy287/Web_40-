using System.ComponentModel.DataAnnotations;
public class NhanVienUpdateDTO
{
    [Required]
    public int MaNhanVien { get; set; }

    [Required]
    [MaxLength(255)]
    public string HoTen { get; set; }

    [Required]
    public bool GioiTinh { get; set; }

    [MaxLength(100)]
    public string? ChucDanh { get; set; }

    [MaxLength(20)]
    public string? SoDienThoai { get; set; }

    [EmailAddress]
    [MaxLength(255)]
    public string? Email { get; set; }

    [MaxLength(20)]
    public string? CaLamViec { get; set; }

    [MaxLength(20)]
    public string? QuyenTruyCap { get; set; }

    public DateTime? NgayTao { get; set; }

    public int? Luong { get; set; }

    [MaxLength(50)]
    public string? TaiKhoan { get; set; }

    [MaxLength(255)]
    public string? MatKhau { get; set; }

    public bool Xoa { get; set; }
}
