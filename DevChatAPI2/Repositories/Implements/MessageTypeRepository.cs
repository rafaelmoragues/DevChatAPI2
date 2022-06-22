using DevChatAPI2.Data;
using DevChatAPI2.Models;
using DevChatAPI2.Repositories.Interfaces;

namespace DevChatAPI2.Repositories.Implements
{
    public class MessageTypeRepository : GenericRepository<MessageType>, IMessageTypeRepository
    {
        public MessageTypeRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
