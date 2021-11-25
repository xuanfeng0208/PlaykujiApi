using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayKuji.Domain.Entities
{
    public class ProductAward
    {
        public int SequenceNo { get; set; }

        [Key]
        public Guid ID { get; set; }

        [Required]
        public Guid ProductID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string PictureUrl { get; set; }

        public int? Count { get; set; }

        [Required]
        [MaxLength(20)]
        public string Award { get; set; }

        [Required]
        public bool IsPlay { get; set; }

        [Required]
        public bool IsGrandPrize { get; set; }

        [Required]
        [MaxLength(50)]
        public string CreateUser { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        [Required]
        [MaxLength(50)]
        public string UpdateUser { get; set; }

        [Required]
        public DateTime UpdateTime { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; } 
    }
}