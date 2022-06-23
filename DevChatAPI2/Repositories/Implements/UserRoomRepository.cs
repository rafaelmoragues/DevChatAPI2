using DevChatAPI2.Data;
using DevChatAPI2.Models;
using DevChatAPI2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevChatAPI2.Repositories.Implements
{
    public class UserRoomRepository : GenericRepository<UserRoom>, IUserRoomRepository
    {
        public UserRoomRepository(ApplicationDbContext db) : base(db)
        {
        }
        public IEnumerable<UserRoom> GetRoomsFull(string id)
        {
            return _db.UserRooms.Where(r => r.UserId == id).Include(y => y.RoomChats).ToList();
        }        
    }
}
