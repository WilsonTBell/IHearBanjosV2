using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IHearBanjos.Models
{
    public class BanjoistFavorite
    {
        public int Id { get; set; }
        public int BanjoistId { get; set; }
        public int TabId { get; set; }
    }
}