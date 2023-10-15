using Catalog.API.Models;
using Catalog.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductDBContext _context;

    public ProductRepository(ProductDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _context
                        .Products
                        .ToListAsync();
    }

    public async Task<Product> GetProduct(int id)
    {
        if (id < 1)
            return new Product();
        var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
        if (product == null)
            return new Product();
        return product;
    }

    public async Task<IEnumerable<Product>> GetProductByName(string name)
    {
        return await _context
                        .Products
                        .Where(p => p.Name.Equals(name))
                        .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
    {
        return await _context
                        .Products
                        .Where(p => p.Category.Equals(categoryName))
                        .ToListAsync();
    }


    public async Task CreateProduct(Product product)
    {
        _context.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        if (product == null || product.Id == 0)
            return false;

        var productdata = await _context.Products.FindAsync(product.Id);
        if (productdata == null)
            return false;
        productdata.Name = product.Name;
        productdata.Description = product.Description;
        productdata.Price = product.Price;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteProduct(int id)
    {
        if (id < 1)
            return false;
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return false;
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}
