using InteraceDemo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelDemo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService) 
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        //[Authorize(Policy = "FindAllPolicy")]
        [Route("FindAll")]
        public async Task<IActionResult> FindAll() 
        {
            string str = Request.HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString() + ":" + Request.HttpContext.Connection.LocalPort;

            var users = await _userService.FindAll();
            var usersList = users.ToList();
            usersList.ForEach(p => p.Info = str);
            return new JsonResult(usersList);
        }

        [HttpGet]
        //[Authorize(Policy = "FindUserPolicy")]
        [Route("FindUser")]
        public async Task<User> FindUser(int id)
        {
            var user = await _userService.FindUser(id);
            user.Info = Request.HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString() + ":" + Request.HttpContext.Connection.LocalPort;
            return user;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            string str = Request.HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString() + ":" + Request.HttpContext.Connection.LocalPort;

            var users = await _userService.FindAll();
            var usersList = users.ToList();
            usersList.ForEach(p => p.Info = str);
            return new JsonResult(usersList);
        }
    }
}
