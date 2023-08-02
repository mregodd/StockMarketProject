﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Business.Abstract;
using StockMarket.Business.Concrete;
using StockMarket.Entities.Concrete;

namespace StockMarket.API.Controllers
{
    [ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IBalanceManager _balanceManager;
    private readonly UserManager<User> _userManager;

    public UserController(IBalanceManager balanceService, UserManager<User> userManager)
    {
            _balanceManager = balanceManager;
        _userManager = userManager;
    }

    [HttpGet("{userId}/balance")]
    public IActionResult GetUserBalance(int userId)
    {
        // Kullanıcının bakiyesini almak için BalanceService'i kullanın
        var balance = _balanceService.GetUserBalance(userId);
        return Ok(balance);
    }
    [Authorize("AdminOnly")]
    [HttpPost("{userId}/addbalance")]
    public IActionResult AddUserBalance(int userId, [FromBody] decimal amount)
    {
        // Kullanıcının bakiyesine amount kadar para eklemek için BalanceService'i kullanın
        _balanceService.AddUserBalance(userId, amount);
        return Ok("Bakiye başarıyla eklendi.");
    }

    [Authorize("AdminOnly")]
    [HttpPost("{userId}/subtractbalance")]
    public IActionResult SubtractUserBalance(int userId, [FromBody] decimal amount)
    {
        // Kullanıcının bakiyesinden amount kadar para düşürmek için BalanceService'i kullanın
        _balanceService.SubtractUserBalance(userId, amount);
        return Ok("Bakiyeden para düşürüldü.");
    }
}

}