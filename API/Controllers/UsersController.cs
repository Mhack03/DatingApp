using System.Security.Claims;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseApiController
{
    [HttpGet] // To get to this controller(Page) we need to go to "localhost:5016/api/users" 
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await userRepository.GetMembersAsync();
        return Ok(users);
    }
    
    [HttpGet("{username}")] // /api/users/2
    public async Task<ActionResult<MemberDto>> GetUsers(string username)
    {
        var user = await userRepository.GetMemberAsync(username);

        if (user == null) return NotFound();
        
        return user;
    }

    [HttpPut]
    public async Task<ActionResult> Updateuser(MemberUpdateDto memberUpdateDto)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if(username == null) return BadRequest("No username found in token");

        var user = await userRepository.GetUserByUsernameAsync(username);

        if(user == null) return BadRequest("Could not find user");

        mapper.Map(memberUpdateDto, user);

        if(await userRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to update the user");
    }
}
