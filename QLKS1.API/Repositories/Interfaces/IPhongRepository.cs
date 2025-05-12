// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

namespace QLKS1.API.Repositories.Interfaces
{
    public interface IPhongRepository
    {
        Task<Phong> GetPhongByIdAsync(int IDPhong);
        Task<List<Phong>> GetAllPhong();
        Task<IEnumerable<Phong>> GetPhongsByTrangThaiAsync(string trangThai);

        Task<PhongThongKe> GetThongKePhongAsync();


    }
}