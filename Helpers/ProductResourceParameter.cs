using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace ProductCoreAPI.Helpers
{
    public class ProductResourceParameter
    {
        const int maxPagesize = 20;

        public int pageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPagesize) ? maxPagesize : value;

            }

        }
        public string Name { get; set; }

        public string SearchQuery { get; set; }        
    }
}