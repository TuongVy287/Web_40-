
namespace QLKS1.API.Repositories.Interfaces
{
  public interface ITimPhongPhuHopRepository
{
    Task<IEnumerable<PhongPhuHop>> TimPhongAsync(DateTime checkIn, DateTime checkOut, int adults, int children);
}
}