using DevChatAPI2.Models;
using Microsoft.EntityFrameworkCore;

namespace DevChatAPI2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<RoomChat> RoomChats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserRoom> UserRooms { get; set; }
        public DbSet<MessageType> MessageType { get; set; }
        public DbSet<CategoryChat> CategoryChats { get; set; }
    }
}
