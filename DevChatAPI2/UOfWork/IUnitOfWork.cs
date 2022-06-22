using DevChatAPI2.Repositories.Interfaces;

namespace DevChatAPI2.UOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IMessageRepository MessageRepository { get; }
        ICatChatRepository CatChatRepository { get; }
        IMessageTypeRepository MessageTypeRepository { get; }
        IRoomChatRepository RoomChatRepository { get; }
        IUserRoomRepository UserRoomRepository { get; }
        void Save();

    }
}
