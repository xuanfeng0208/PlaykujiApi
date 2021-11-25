using Microsoft.EntityFrameworkCore;
using PlayKuji.Domain.Entities;
using PlayKuji.Domain.Interfaces.Repositories;

namespace PlayKuji.DataLayer.Repositorys
{
    public class ProcuctRepository : Repository<Product>, IProductRepository
    {
        public ProcuctRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}