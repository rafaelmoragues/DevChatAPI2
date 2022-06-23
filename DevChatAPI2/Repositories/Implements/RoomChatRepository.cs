using DevChatAPI2.Data;
using DevChatAPI2.Models;
using DevChatAPI2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevChatAPI2.Repositories.Implements
{
    public class RoomChatRepository : GenericRepository<RoomChat>, IRoomChatRepository
    {
        public RoomChatRepository(ApplicationDbContext db) : base(db)
        {            
        }       
        
        public RoomChat GetRoomFull(int id)
        {
            RoomChat res = _db.RoomChats.Where(r => r.Id == id).Include(r => r.Messages).FirstOrDefault();
            var mes = res.Messages.OrderBy(x => x.Date).ToList();
            res.Messages = mes;
            return res;
        }
    }
}
