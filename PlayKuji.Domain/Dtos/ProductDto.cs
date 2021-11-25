using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PlayKuji.Domain.Dtos
{
    public class ProductDto
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public string LinkUrl { get; set; }

        public int? Count { get; set; }

        public decimal? Price { get; set; }

        public DateTime? SaleDate { get; set; }

        public string Tags { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<ProductAward> ProductAwards { get; set; }

        public class ProductAward
        {
            [JsonIgnore]
            public Guid ID { get; set; }

            public string Name { get; set; }

            public string PictureUrl { get; set; }

            public int? Count { get; set; }

            public string Award { get; set; }

            public bool IsPlay { get; set; }

            public bool IsGrandPrize { get; set; }
        }
    }
}