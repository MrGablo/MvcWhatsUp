using MvcWhatsUp.Models;

namespace MvcWhatsUp.Repositories
{
    public interface IUsersRepository
    {
        List<User> GetAll();
        User? GetbyId(int userId);
        void Add(User user);
        void Update(User user);
        void Delete(User user);
        User? GetByLoginCredentials(string userName, string password);
    }
}
