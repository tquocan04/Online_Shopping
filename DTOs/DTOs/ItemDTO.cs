﻿using Entities.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTOs.DTOs
{
    public class ItemDTO
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
