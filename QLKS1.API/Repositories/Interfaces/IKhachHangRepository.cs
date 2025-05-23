using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKS1.API.Repositories.Interfaces
{
    public interface IKhachHangRepository
    {
        Task<KhachHang> GetKhachHangByIdAsync(int maKH);
        Task<List<KhachHang>> GetAllKhachHang();

        Task<KhachHangDatPhongViewModel> GetChiTietKhachHangAsync(int idKhachHang, int idDatPhong);

        Task<IEnumerable<GuestInfo>> GetGuestInfoAsync();   

        Task<int> ThemKhachHang(KhachHang khachHang); // Thêm dòng này

    }
}