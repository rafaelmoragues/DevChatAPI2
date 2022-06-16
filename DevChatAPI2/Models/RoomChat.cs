﻿using System.ComponentModel.DataAnnotations;

namespace DevChatAPI2.Models
{
    public class RoomChat
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public CategoryChat Category { get; set; }
    }
}
