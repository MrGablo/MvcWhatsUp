using Microsoft.Data.SqlClient;
using MvcWhatsUp.Models;
using System.Windows.Input;
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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"SELECT UserId, UserName, MobileNumber, EmailAddress FROM Users WHERE UserId = {userId}";
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User user = ReadUser(reader);
                    return user;
                }
            }
            throw new NotImplementedException();
        }

        void IUsersRepository.Add(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"INSERT INTO Users(UserName, MobileNumber, EmailAddress)" +
                    "VALUES (@UserName, @MobileNumber, @EmailAddress)" +
                    "SELECT SCOPE_IDENTITY();";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
                command.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);

                command.Connection.Open();
                user.UserId = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        void IUsersRepository.Update(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Users SET UserName = @UserName, MobileNumber = @MobileNumber," +
                    "EmailAddress = @EmailAddress WHERE UserId = @Id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Id", user.UserId);
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
                command.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);

                command.Connection.Open();
                int nrOfRowsAffected = command.ExecuteNonQuery();
                if (nrOfRowsAffected == 0)
                    throw new Exception("No records updated!");
            }
        }

        void IUsersRepository.Delete(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"DELETE FROM Users WHERE UserId = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", user.UserId);

                command.Connection.Open();
                int nrOfRowsAffected = command.ExecuteNonQuery();
                if(nrOfRowsAffected == 0)
                {
                    throw new Exception("No records deleted!");
                }
            }
        }

        User? IUsersRepository.GetByLoginCredentials(string userName, string password)
        {
            //get user (from repository)
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"SELECT UserId, UserName, MobileNumber, EmailAddress FROM Users WHERE UserName = @UserName AND Password = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Password",password);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User user = ReadUser(reader);
                    return user;
                }
            }
            return null;
        }
    }
}
