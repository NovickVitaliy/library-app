using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace OrderAndInventory.API.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public abstract class BaseApiController
{
    
}