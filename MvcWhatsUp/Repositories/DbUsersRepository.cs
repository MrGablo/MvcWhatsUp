using Microsoft.Data.SqlClient;
using MvcWhatsUp.Models;
namespace MvcWhatsUp.Repositories
{
    public class DbUsersRepository : IUsersRepository
    {
        private readonly string? _connectionString;
        public DbUsersRepository(IConfiguration configuration)
        {
            //get (database) connection from appsettings
            _connectionString = configuration.GetConnectionString("WhatsUpDatabase");
        }
        private User ReadUser(SqlDataReader reader)
        {
            //retrieve data from fields
            int id = (int)reader["UserId"];
            string name = (string)reader["UserName"];
            string mobileNumber = (string)reader["MobileNumber"];
            string emailAddress = (string)reader["EmailAddress"];

            //string password = (string)reader["Password"]

            //return new User object
            return new User(id, name, mobileNumber, emailAddress);
        }
        //public List<User> GetAll()
        //{
        //    List<User> users = new List<User>();

        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        string query = "SELECT UserId, UserName, MobileNumber, EmailAddress FROM Users";
        //        SqlCommand command = new SqlCommand(query, connection);

        //        command.Connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            User user = ReadUser(reader);
        //            users.Add(user);
        //        }
        //        reader.Close();
        //    }
        //    return users;
        //}

        List<User> IUsersRepository.GetAll()
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT UserId, UserName, MobileNumber, EmailAddress FROM Users";
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    User user = ReadUser(reader);
                    users.Add(user);
                }
                reader.Close();
            }
            return users;
        }

        User? IUsersRepository.GetbyId(int userId)
        {
            throw new NotImplementedException();
        }

        void IUsersRepository.Add(User user)
        {
            throw new NotImplementedException();
        }

        void IUsersRepository.Update(User user)
        {
            throw new NotImplementedException();
        }

        void IUsersRepository.Delete(User user)
        {
            throw new NotImplementedException();
        }
    }
}
