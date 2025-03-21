using Microsoft.Data.SqlClient;
using MvcWhatsUp.Models;

namespace MvcWhatsUp.Repositories
{
    public class DbChatsRepository : IChatsRepository
    {
        private readonly string? _connectionString;
        public DbChatsRepository(IConfiguration configuration)
        {
            //get (database) connection from appsettings
            _connectionString = configuration.GetConnectionString("WhatsUpDatabase");
        }
        public void AddMessage(Message message)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"INSERT INTO Chats(SenderUserId, ReceiverUserId, Message, SendAt)" +
                    "VALUES (@SenderUserId, @ReceiverUserId, @Message, @SendAt)" +
                    "SELECT SCOPE_IDENTITY();";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@SenderUserId", message.SenderUserId);
                command.Parameters.AddWithValue("@ReceiverUserId", message.ReceiverUserId);
                command.Parameters.AddWithValue("@Message", message.MessageText);
                command.Parameters.AddWithValue("@SendAt", message.SendAt);

                command.Connection.Open();
                message.MessageId = Convert.ToInt32(command.ExecuteScalar());
            }
            throw new NotImplementedException();
        }

        public List<Message> GetMessages(int userId1, int userId2)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT MessageId, SenderUserId, ReceiverUserId, Message, SendAt FROM Messages "
                + "WHERE (SenderUserId = @UserId1 AND ReceiverUserId = @UserId2) OR "
                + "(SenderUserId = @UserId2 AND ReceiverUserId = @UserId1) "
                + "ORDER BY SendAt ASC";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId1", userId1);
                command.Parameters.AddWithValue("@UserId2", userId2);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

            }
            throw new NotImplementedException();
        }
    }
}
