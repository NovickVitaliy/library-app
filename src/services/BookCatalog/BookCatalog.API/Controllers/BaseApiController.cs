using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.API.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public abstract class BaseApiController : ControllerBase
{
    
}