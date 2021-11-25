using Microsoft.Extensions.Configuration;
using PlayKuji.Domain.Dtos;
using PlayKuji.Domain.Entities;
using PlayKuji.Domain.Interfaces.Logics;
using PlayKuji.Domain.Interfaces.Managers;
using PlayKuji.Domain.Interfaces.Repositories;
using PlayKuji.Domain.Models.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PlayKuji.BusinessLayer.Logics
{
    public class ProductLogic : IProductLogic
    {
        readonly IProductRepository _productRepository;
        readonly ICrawlerManager _crawlerManager;
        readonly IConfiguration _configuration;

        public ProductLogic(
            IProductRepository productRepository,
            ICrawlerManager crawlerManager,
            IConfiguration configuration)
        {
            _productRepository = productRepository;
            _crawlerManager = crawlerManager;
            _configuration = configuration;
        }

        public void Create()
        {
            var url = _configuration["KujiOfficeUrl"];
            var indexHtml = _crawlerManager.GetHtml($"{url}/cn/kuji/index.html");
            var indexMatches = Regex.Matches(indexHtml, "<figure.*?>.*?<a href=\"(.*?)\" class=\"-iframe\">.*?<img class=\"thumb\" src=\"(.*?)\">.*?<p class=\"copy\">(.*?)</p>.*?</figure>", RegexOptions.Singleline);
            var isNotPlay = new string[] { "LAST", "雙重" };
            var isNotProduct = new string[] { "造型傘", "盒玩賞" };
            foreach (Match indexMatch in indexMatches)
            {
                var productUrl = indexMatch.Groups[1].Value;
                var productPictureUrl = indexMatch.Groups[2].Value;
                var productName = indexMatch.Groups[3].Value;
                if (_productRepository.Any(x => x.Name == productName) || isNotProduct.Any(x => productName.Contains(x)))
                    continue;

                var productID = Guid.NewGuid();
                var productHtml = _crawlerManager.GetHtml($"https://www.banpresto.jp{productUrl}");
                var productMatch = Regex.Match(productHtml, "<section class=\"product-content\">(.*?)</div>", RegexOptions.Singleline);
                var awardMatches = Regex.Matches(productMatch.Value, "<figure.*?>.*?<img class=\"thumb\" src=\"(.*?)\">.*?<p class=\"copy\">(.*?)</p>.*?</figure>", RegexOptions.Singleline);

                var productAwardList = new List<ProductAward>();
                foreach (Match awardMatch in awardMatches)
                {
                    var awardPictureUrl = awardMatch.Groups[1].Value;
                    var awardInfo = awardMatch.Groups[2].Value;
                    //awardInfo = "A獎角色模型";
                    var awardInfos = awardInfo.Split(new[] { "獎", ")" }, StringSplitOptions.None);
                    var award = awardInfos[0];
                    var awardName = awardInfos[1];
                    productAwardList.Add(new ProductAward
                    {
                        ID = Guid.NewGuid(),
                        ProductID = productID,
                        Name = awardName.Trim(),
                        PictureUrl = $"{url}{awardPictureUrl}",
                        Award = award.TrimStart(),
                        IsPlay = !isNotPlay.Any(x => award.ToUpper().Contains(x)),
                        IsGrandPrize = true,
                        CreateUser = "Admin",
                        CreateTime = DateTime.UtcNow,
                        UpdateUser = "Admin",
                        UpdateTime = DateTime.UtcNow,
                    });
                }

                _productRepository.Create(new Product
                {
                    ID = Guid.NewGuid(),
                    Name = productName,
                    PictureUrl = $"{url}{productPictureUrl}",
                    LinkUrl = $"{url}{productUrl}",
                    Tags = string.Empty,
                    CreateUser = "Admin",
                    CreateTime = DateTime.UtcNow,
                    UpdateUser = "Admin",
                    UpdateTime = DateTime.UtcNow,
                    ProductAwards = productAwardList,
                });
            }
        }

        public ProductDto GetByID(Guid id)
        {
            var product = _productRepository.FirstOrDefault(x => x.IsEnabled && x.ID == id);
            if (product == null)
                return null;

            return new ProductDto
            {
                ID = product.ID,
                Name = product.Name,
                PictureUrl = product.PictureUrl,
                LinkUrl = product.LinkUrl,
                Count = product.Count,
                Price = product.Price,
                SaleDate = product.SaleDate,
                Tags = product.Tags,
                ProductAwards = product.ProductAwards.Select(x => new ProductDto.ProductAward
                {
                    ID = x.ID,
                    Name = x.Name,
                    PictureUrl = x.PictureUrl,
                    Award = x.Award,
                    Count = x.Count,
                    IsPlay = x.IsPlay,
                    IsGrandPrize = x.IsGrandPrize
                }).OrderByDescending(x => x.IsPlay).ThenBy(x => x.Award),
            };
        }

        public List<ProductDto> GetList(ProductSearchModel searchModel)
        {
            var products = _productRepository.Where(x => x.IsEnabled);

            if (!string.IsNullOrWhiteSpace(searchModel.Search))
                products = products.Where(x => x.Name.Contains(searchModel.Search));

            if (searchModel.Year.HasValue)
                products = products.Where(x => x.SaleDate.Value.Year == searchModel.Year.Value);

            if (searchModel.Month.HasValue)
                products = products.Where(x => x.SaleDate.Value.Month == searchModel.Month.Value);

            return products.OrderByDescending(x => x.SaleDate).Skip((searchModel.Page - 1) * searchModel.PageSize).Take(searchModel.PageSize).Select(x => new ProductDto
            {
                ID = x.ID,
                Name = x.Name,
                PictureUrl = x.PictureUrl,
                LinkUrl = x.LinkUrl,
                Count = x.Count,
                Price = x.Price,
                SaleDate = x.SaleDate,
                Tags = x.Tags,
            }).ToList();
        }
    }
}