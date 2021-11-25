namespace PlayKuji.Domain.Models.SearchModel
{
    public class ProductSearchModel: SearchModel
    {
        public string Search { get; set; }

        public int? Year { get; set; }

        public int? Month { get; set; }
    }
}