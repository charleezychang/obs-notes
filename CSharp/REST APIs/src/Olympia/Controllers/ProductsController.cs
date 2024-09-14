using Microsoft.AspNetCore.Mvc;

namespace Olympia.Controllers;

[ApiController] // dont need to put [FromBody]
[Route("[controller]")]
public class ProductsController(ProductsService productService) : ControllerBase
{
    private readonly ProductsService _productsService = productService ?? throw new ArgumentNullException(nameof(productService));

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {
        // create the product, map to internal representation
        // take note that this is a class so use braces
        // use services to create the product
        // var product = new Product
        // {
        //     Name = request.Name,
        //     Category = request.Category,
        //     SubCategory = request.SubCategory
        // };
        var product = request.ToDomain();
        await _productsService.CreateAsync(product);

        // map to external representationm you dont want to return the incoming request, nor the internal representation of the product
        // records use parenthesis for the constructor
        // FromDomain is used instead
        // var response = new ProductResponse(
        //     product.Id,
        //     product.Name,
        //     product.Category,
        //     product.SubCategory
        // );

        // return the product
        return CreatedAtAction(
            // method
            actionName: nameof(Get),
            // parameters needed for this method
            routeValues: new { ProductId = product.Id },
            value: ProductResponse.FromDomain(product)
        );
    }

    [HttpGet("{productId:guid}")]
    public async Task<IActionResult> Get(Guid productId)
    {
        // get the product
        var product = await _productsService.GetAsync(productId);
        // return the product
        return product is null
            // ? NotFound()
            ? Problem(statusCode: StatusCodes.Status404NotFound, title: "Product not found")
            : Ok(ProductResponse.FromDomain(product));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productsService.GetAllAsync();
        return Ok(products.Select(ProductResponse.FromDomain));
    }

    [HttpPut("{productId:guid}")]
    public async Task<IActionResult> Update(Guid productId, CreateProductRequest product)
    {
        var products = await _productsService.UpdateAsync(productId, product.ToDomain());
        if (!products)
        {
            return Problem(statusCode: StatusCodes.Status404NotFound, title: "Product not found");
        }
        return NoContent();
    }

    [HttpDelete("{productId:guid}")]
    public async Task<IActionResult> Delete(Guid productId)
    {
        var product = await _productsService.DeleteAsync(productId);
        if (!product)
        {
            return Problem(statusCode: StatusCodes.Status404NotFound, title: "Product not found");
        }
        return NoContent();
    }

    public record CreateProductRequest(string Name, string Category, string SubCategory){
        public Product ToDomain()
        {
            return new Product
            {
                Name = Name,
                Category = Category,
                SubCategory = SubCategory
            };
        }
    };

    public record ProductResponse(Guid Id, string Name, string Category, string SubCategory)
    {
        public static ProductResponse FromDomain(Product product)
        {
            return new ProductResponse(
                product.Id,
                product.Name,
                product.Category,
                product.SubCategory
            );
        }
    };
}