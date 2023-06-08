using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("users")]

public class UserController : ControllerBase{
    private readonly MyDbContext _context;

    public UserController(MyDbContext context)
    {
        _context = context;
    }   

  

    [HttpGet]
    public IEnumerable<User> Get(){
        return _context.Users.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        var user = _context.Users.Find(id);
        
        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpPost]
    public ActionResult<User> PostUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();

        return CreatedAtAction("GetUser", new { id = user.Id }, user);
    }

 [HttpPut("{id}")]
public IActionResult PutUser(int id, UpdateUserDTO user)
{
    var userToUpdate = _context.Users.Find(id);
    if (userToUpdate == null)
    {
        return NotFound();
    }

    userToUpdate.Name = user.Name;
    userToUpdate.Email = user.Email;

    _context.Users.Update(userToUpdate);
    _context.SaveChanges();

    return NoContent();
}

    
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        _context.SaveChanges();

        return NoContent();
    }
}
