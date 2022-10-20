using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swiggy.Core.IRepository;
using Swiggy.Data;
using Swiggy.Models;

namespace Swiggy.Core.Repository
{
    public class ProductRepository :IProductRepository
    {
        private readonly SwiggyDbContext dbContext;
        private DevExpress.Xpo.UnitOfWork uow;

        public ProductRepository(SwiggyDbContext swiggyDbContext)
        {
            dbContext = swiggyDbContext;
        }

        public ProductRepository(DevExpress.Xpo.UnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<List<ProductsModel>> GetProducts()
        {
            try
            {

                var result = await dbContext.Products.ToListAsync();
                return (result);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<ProductsModel> AddProduct(AddProductModel product)
        {
            try
            {
                var product1 = new ProductsModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                };
                await dbContext.Products.AddAsync(product1);
                dbContext.SaveChanges();
                return product1;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<ProductsModel> DeleteProduct(Guid id)
        {
            try
            {
                var product = await dbContext.Products.FindAsync(id);
                if (product == null)
                    return null;
                dbContext.Products.Remove(product);
                dbContext.SaveChanges();
                return(product);
            }
            catch(Exception e)
            {
                throw e;
            }

        }
        public async Task<ProductsModel> UpdateProduct(Guid id, AddProductModel addProductModel)
        {
            try
            {
                var product = await dbContext.Products.FindAsync(id);
                if (product == null)
                    return null;
                product.ProductId = id;
                product.ProductName = addProductModel.ProductName;
                product.ProductPrice = addProductModel.ProductPrice;
                await dbContext.SaveChangesAsync();
                return(product);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
