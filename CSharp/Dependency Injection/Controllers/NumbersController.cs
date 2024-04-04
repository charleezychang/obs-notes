using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class NumbersController : ControllerBase
{
    private INumbersTransientService _numbersTransientService;
    private INumbersSingletonService _numbersSingletonService;

    public NumbersController(INumbersTransientService numbersTransientService, INumbersSingletonService numbersSingletonService)
    {
        _numbersTransientService = numbersTransientService;
        _numbersSingletonService = numbersSingletonService;
    }

    [HttpGet("get-number")]
    public IActionResult GetNumber()
    {
        var result = new Lifetime
        {
            Transient = _numbersTransientService.GetNumber(),
            Singleton = _numbersSingletonService.GetNumber()
        };
        return Ok(result);
    }
}