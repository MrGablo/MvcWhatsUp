using MvcWhatsUp.Models;

namespace MvcWhatsUp.Repositories
{
    public class DummyUsersRepositor : IUsersRepository
    {
        // a hardcoded list of users
        private static List<User> users =
        [
        new User(1, "Peter Sauber", "06-87763419", "peter.sauber@gmail.com"),
        new User(2, "Bill Hodges", "06-14022398", "bill.hodges@gmail.com"),
        new User(3, "Morris Bellamy", "06-56190265", "morris.bellamy@gmail.com"),
        ];

        void IUsersRepository.Add(User user)
        {
            users.Add(user);
        }

        void IUsersRepository.Delete(User user)
        {
            User? existingUser = users.FirstOrDefault(u => u.UserId == user.UserId);
            if (existingUser is not null)
            {
                users.Remove(existingUser);
            }
        }

        List<User> IUsersRepository.GetAll()
        {
            return users;
        }

        User? IUsersRepository.GetbyId(int userId)
        {
            return users.FirstOrDefault(x => x.UserId == userId);
        }

        void IUsersRepository.Update(User user)
        {
            User? existingUser = users.FirstOrDefault(u => u.UserId == user.UserId);
            if(existingUser is not null)
            {
                existingUser.UserName = user.UserName;
                existingUser.MobileNumber = user.MobileNumber;
                existingUser.EmailAddress = user.EmailAddress;
            }
        }


        // ...
    }

}
