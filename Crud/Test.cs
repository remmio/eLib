using System.Threading.Tasks;
using eLib.Entity;
using eLib.Exceptions;

namespace eLib.Crud
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly int _param;

        public ProductService(IRepository<Product> repository, int param)
            : base(repository)
        {
            _param = param;
        }

        public override Task<Operation> Add(Product product)
        {
            //if (_repository.ContainsName(product.Name))
            //{
            //    throw new ArgumentException(string.Format("There is already a product with the name '{0}'.", product.Name), "product");
            //}

            //if (product.Name == null || product.Name.Equals(string.Empty))
            //{
            //    throw new ArgumentException("Product name cannot be null or empty string.", nameof(product));
            //}

           return base.Add(product);
        }
    }

    internal interface IProductService
    {
    }

    public class Product:BaseEntity
    {

    }
}
