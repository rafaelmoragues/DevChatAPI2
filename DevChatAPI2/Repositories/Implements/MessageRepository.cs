using DevChatAPI2.Data;
using DevChatAPI2.Models;
using DevChatAPI2.Repositories.Interfaces;

namespace DevChatAPI2.Repositories.Implements
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
