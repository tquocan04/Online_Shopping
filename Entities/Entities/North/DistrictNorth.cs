﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Entities.North
{
    public class DistrictNorth
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public ICollection<AddressNorth>? Addresses { get; set; }
        [ForeignKey(nameof(City))]
        public Guid CityId { get; set; }
        public CityNorth? City { get; set; }
    }
}