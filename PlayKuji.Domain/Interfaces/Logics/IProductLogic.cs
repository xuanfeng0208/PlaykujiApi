using PlayKuji.Domain.Dtos;
using PlayKuji.Domain.Models.SearchModel;
using System;
using System.Collections.Generic;

namespace PlayKuji.Domain.Interfaces.Logics
{
    public interface IProductLogic
    {
        ProductDto GetByID(Guid id);

        List<ProductDto> GetList(ProductSearchModel searchModel);

        void Create();
    }
}