﻿using System.ComponentModel.DataAnnotations.Schema;

namespace FinShark.Models
{
    [Table("Portfolio")]
    public class Portfolio
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }
    }
}
