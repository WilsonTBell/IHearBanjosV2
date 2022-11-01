using System;
using System.Buffers.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IHearBanjos.Models
{
    public class Tab
    {
        public int Id { get; set; }


        [Required]
        public string Title { get; set; }


        [Required]
        public string ImageLocation { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DisplayName("Type")]
        public int? TypeId { get; set; }
        public Type Type { get; set; }

        [Required]
        [DisplayName("Difficulty")]
        public int? DifficultyId { get; set; }
        public Difficulty Difficulty { get; set; }


        [DisplayName("Banjoist")]
        public int BanjoistId { get; set; }
        public Banjoist Banjoist { get; set; }
    }
}
