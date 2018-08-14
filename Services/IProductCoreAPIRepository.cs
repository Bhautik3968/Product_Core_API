using System.Collections;
using ProductCoreAPI.Models;
using ProductCoreAPI.Helpers;
using System.Collections.Generic;

namespace ProductCoreAPI.Services
{
    public interface IProductCoreAPIRepository
    {
        PagedList<Product> GetProducts(ProductResourceParameter productResourceParameter);
        Product GetProduct(int productId);
        void AddProduct(Product product);
        void DeleteProduct(Product product);
       /*  void UpdateProduct(Product product); */
        bool ProductExists(int productId);
        IEnumerable<Error> GetErrors();
        Error GetError(int errorId);
        void AddError(Error error);  
        User GetUser(int userId); 
        IEnumerable<User> GetUsers();
        JwtToken GetToken(int userId);
        bool Save();
    }
}