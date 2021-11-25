using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayKuji.Domain.Entities
{
    public class Product
    {
        public int SequenceNo { get; set; } 

        [Key]
        public Guid ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string PictureUrl { get; set; }

        [Required]
        [MaxLength(300)]
        public string LinkUrl { get; set; }

        public int? Count { get; set; }

        public decimal? Price { get; set; }

        public DateTime? SaleDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Tags { get; set; }

        [Required]
        public bool IsEnabled { get; set; }

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

        public virtual IEnumerable<ProductAward> ProductAwards { get; set; }
    }
}