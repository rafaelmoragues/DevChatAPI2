using DevChatAPI2.Models;

namespace DevChatAPI2.Repositories.Interfaces
{
    public interface IUserRoomRepository :IGenericRepository<UserRoom>
    {
        IEnumerable<UserRoom> GetRoomsFull(string id);
    }
}
