using Microsoft.EntityFrameworkCore;
using PlayKuji.Domain.Entities;
using PlayKuji.Domain.Interfaces.Repositories;

namespace PlayKuji.DataLayer.Repositorys
{
    public class ProductAwardRepository : Repository<ProductAward>, IProductAwardRepository
    {
        public ProductAwardRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}