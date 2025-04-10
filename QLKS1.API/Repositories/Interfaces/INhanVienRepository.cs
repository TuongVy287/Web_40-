using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKS1.API.Repositories.Interfaces
{
    public interface INhanVienRepository
    {
        Task<List<NhanVien>> GetAllNhanVienAsync();
        Task<bool> CreateNhanVienAsync(NhanVienCreateDTO dto);
        Task<bool> UpdateNhanVienAsync(NhanVienUpdateDTO dto);
    }
}