using DevChatAPI2.Models;

namespace DevChatAPI2.Repositories.Interfaces
{
    public interface IRoomChatRepository : IGenericRepository<RoomChat>
    {
        RoomChat GetRoomFull(int id);
    }
}
