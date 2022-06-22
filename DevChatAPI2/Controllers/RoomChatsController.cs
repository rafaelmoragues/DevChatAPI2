using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevChatAPI2.Data;
using DevChatAPI2.Models;
using DevChatAPI2.Services.Interfaces;

namespace DevChatAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomChatsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IRoomChatService _roomChatService;

        public RoomChatsController(ApplicationDbContext context, IRoomChatService roomS)
        {
            _context = context;
            _roomChatService = roomS;
        }

        // GET: api/RoomChats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomChat>>> GetRoomChats()
        {
          if (_context.RoomChats == null)
          {
              return NotFound();
          }
            return await _context.RoomChats.ToListAsync();
        }

        // GET: api/RoomChats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomChat>> GetRoomChat(int id)
        {
          if (_context.RoomChats == null)
          {
              return NotFound();
          }
            var roomChat = await _context.RoomChats.FindAsync(id);

            if (roomChat == null)
            {
                return NotFound();
            }

            return roomChat;
        }
        [HttpGet("/api/[controller]/priv")]
        public IActionResult GetPrivateChat([FromQuery] string idSender, [FromQuery] string idReceiver)
        {
            var room = _roomChatService.GetRoomChat(idSender, idReceiver);
            return Ok(room);
        }

        // PUT: api/RoomChats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomChat(int id, RoomChat roomChat)
        {
            if (id != roomChat.Id)
            {
                return BadRequest();
            }

            _context.Entry(roomChat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomChatExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RoomChats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomChat>> PostRoomChat(RoomChat roomChat, [FromQuery] string idSender, [FromQuery] string idReceiver)
        {
          if (_context.RoomChats == null)
          {
              return Problem("Entity set 'ApplicationDbContext.RoomChats'  is null.");
          }
          int auxId =_roomChatService.AddRoomChat(roomChat);
            _roomChatService.AddUserRoom(idSender, auxId);
            _roomChatService.AddUserRoom(idReceiver, auxId);
            //_context.RoomChats.Add(roomChat);
            //await _context.SaveChangesAsync();
            return CreatedAtAction("GetRoomChat", new { id = roomChat.Id }, roomChat);
        }

        // DELETE: api/RoomChats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomChat(int id)
        {
            if (_context.RoomChats == null)
            {
                return NotFound();
            }
            var roomChat = await _context.RoomChats.FindAsync(id);
            if (roomChat == null)
            {
                return NotFound();
            }

            _context.RoomChats.Remove(roomChat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomChatExists(int id)
        {
            return (_context.RoomChats?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
