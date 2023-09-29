using CachingwithRedis.Models;
using CachingwithRedis.Repos;
using Microsoft.AspNetCore.Mvc;

namespace CachingwithRedis.Controllers;

[ApiController]
[Route("[controller]")]
public class MemberController : ControllerBase
{
  private readonly ILogger<MemberController> _logger;
  private readonly IMemberRepository _member;

    public MemberController(ILogger<MemberController> logger, IMemberRepository member)
    {
        _logger = logger;
        _member = member;
    }

    [HttpGet(Name = "Get")]
    public async Task<Member> Get(int Id)
    {
       return await _member.GetById(Id);
    }
}