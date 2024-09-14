using Microsoft.EntityFrameworkCore;

using Olympia.Persistence;

public class ProductsService(OlympiaContext dbContext)
{
    private readonly OlympiaContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task CreateAsync(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Product?> GetAsync(Guid productId)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(product => product.Id == productId);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task<Boolean> UpdateAsync(Guid productId, Product product)
    {
        try
        {
            var productToUpdate = await _dbContext.Products.FirstOrDefaultAsync(product => product.Id == productId);
            if (productToUpdate is null)
            {
                return false;
            }
            productToUpdate.Name = product.Name;
            productToUpdate.Category = product.Category;
            productToUpdate.SubCategory = product.SubCategory;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
        catch (Exception _ex)
        {
            return false;
        }
    }

    public async Task<Boolean> DeleteAsync(Guid productId)
    {
        try
        {
            var productToDelete = await _dbContext.Products.FirstOrDefaultAsync(product => product.Id == productId);
            if (productToDelete is null)
            {
                return false;
            }
            _dbContext.Products.Remove(productToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
        catch (Exception _ex)
        {
            return false;
        }
}