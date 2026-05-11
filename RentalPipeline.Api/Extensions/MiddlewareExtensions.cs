using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RentalPipeline.API.Extensions
{
    [Route("api/[controller]")]
    [ApiController]
    public class MiddlewareExtensions : ControllerBase
    {
    }
}
