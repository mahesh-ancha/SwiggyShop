using Swiggy.Models;

namespace Swiggy.Core.IRepository
{
    public interface IProductRepository
    {
        Task<List<ProductsModel>> GetProducts();
        Task<ProductsModel> AddProduct(AddProductModel product);
        Task<ProductsModel> UpdateProduct(Guid id, AddProductModel addProductModel);
        Task<ProductsModel> DeleteProduct(Guid id);
    }
}
