using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKS1.API.Repositories.Interfaces
{
    public interface INhanVienRepository
    {
        Task<List<NhanVien>> GetAllNhanVienAsync();
        Task<bool> SuaNhanVienAsync(NhanVien nhanVien);

        Task<bool> XoaNhanVienAsync(string maNV);

        Task<NhanVien> GetByMaNVAsync(string maNV);
        Task<bool> ChangePasswordAsync(string maNV, string currentPassword, string newPassword);

    }
}