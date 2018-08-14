using ProductCoreAPI.Models;
using ProductCoreAPI.DBContext;
using System.Collections.Generic;
using System.Linq;
using System;
using ProductCoreAPI.Helpers;
namespace ProductCoreAPI.Services
{
    public class ProductCoreAPIRepository : IProductCoreAPIRepository
    {
        private ProductCoreAPIContext _context;
        public ProductCoreAPIRepository(ProductCoreAPIContext context)
        {
            _context = context;
        }
        public PagedList<Product> GetProducts(ProductResourceParameter productResourceParameter)
        {
            var collectionBeforePaging = _context.Product.OrderBy(x => x.Name).AsQueryable();
            if (!string.IsNullOrWhiteSpace(productResourceParameter.Name))
            {
                var nameForWhereClause = productResourceParameter.Name.Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging.Where(x => x.Name.ToLowerInvariant() == nameForWhereClause);
            }
            if (!string.IsNullOrWhiteSpace(productResourceParameter.SearchQuery))
            {
                var searchQueryWhereCaluse = productResourceParameter.SearchQuery.Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging.Where(x => x.Name.ToLowerInvariant().Contains(searchQueryWhereCaluse)
                || x.Price.Contains(searchQueryWhereCaluse));
            }
            return PagedList<Product>.Create(collectionBeforePaging, productResourceParameter.pageNumber, productResourceParameter.PageSize);
        }
        public Product GetProduct(int productId)
        {
            return _context.Product.FirstOrDefault(x => x.ID == productId);
        }
        public void AddProduct(Product product)
        {
            _context.Product.Add(product);
        }
        public void DeleteProduct(Product product)
        {
            _context.Product.Remove(product);
        }
        /* public void UpdateProduct(Product product)
        {
            _context.Product.Update(product);
        } */
        public bool ProductExists(int productId)
        {
            return _context.Product.Any(x => x.ID == productId);
        }
        public IEnumerable<Error> GetErrors()
        {
            return _context.API_Errors.OrderBy(x => x.ID);
        }
        public Error GetError(int errorId)
        {
            return _context.API_Errors.FirstOrDefault(X => X.ID == errorId);
        }
        public void AddError(Error error)
        {
            _context.Add(error);
        }
        public User GetUser(int userId)
        {
            return _context.tblUsers.FirstOrDefault(x => x.ID == userId);
        }
        public IEnumerable<User> GetUsers()
        {
            return _context.tblUsers.OrderBy(x => x.ID);
        }
        public JwtToken GetToken(int userId)
        {
            var token = new JwtTokenBuilder()
                                           .AddSecurityKey(JwtSecurityKey.Create("ProductCoreAPI-secret-key"))
                                           .AddSubject("Test")
                                           .AddIssuer("ProductCoreAPI.Bearer")
                                           .AddAudience("ProductCoreAPI.Bearer")
                                           .AddClaim("UserId", userId.ToString())
                                           .AddExpiry(30)
                                           .Build();
            return token;
        }
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}