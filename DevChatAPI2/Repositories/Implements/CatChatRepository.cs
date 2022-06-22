using DevChatAPI2.Data;
using DevChatAPI2.Models;
using DevChatAPI2.Repositories.Interfaces;

namespace DevChatAPI2.Repositories.Implements
{
    public class CatChatRepository : GenericRepository<CategoryChat>, ICatChatRepository
    {
        public CatChatRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
