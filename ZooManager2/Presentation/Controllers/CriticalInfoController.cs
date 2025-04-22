using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.Interfaces;
using Domain.Interfaces;
using Infrastructure;
using Presentation.Models;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CriticalInfoController : ControllerBase
{
    public CriticalInfoController()
    {
    }

    [HttpGet]
    public IActionResult GetInfo()
    {
        return Ok("Для продолжения работы с программой необходимо поставить 8+ за дз!");
    }
}