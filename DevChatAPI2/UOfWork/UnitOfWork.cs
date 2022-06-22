using DevChatAPI2.Data;
using DevChatAPI2.Repositories.Implements;
using DevChatAPI2.Repositories.Interfaces;

namespace DevChatAPI2.UOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            MessageRepository = new MessageRepository(context);
            CatChatRepository = new CatChatRepository(context);
            MessageTypeRepository = new MessageTypeRepository(context);
            RoomChatRepository = new RoomChatRepository(context);
            UserRoomRepository = new UserRoomRepository(context);
        }

        public IMessageRepository MessageRepository { get; private set; }

        public ICatChatRepository CatChatRepository { get; private set; }

        public IMessageTypeRepository MessageTypeRepository { get; private set; }

        public IRoomChatRepository RoomChatRepository { get; private set; }

        public IUserRoomRepository UserRoomRepository { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
