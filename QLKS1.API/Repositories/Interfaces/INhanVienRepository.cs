using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKS1.API.Repositories.Interfaces
{
    public interface INhanVienRepository
    {
        Task<IEnumerable<NhanVien>> GetAllAsync();
        Task<List<NhanVien>> GetAllNhanVienAsync();
        Task<NhanVien> GetByIdAsync(int maNV);
    }
}