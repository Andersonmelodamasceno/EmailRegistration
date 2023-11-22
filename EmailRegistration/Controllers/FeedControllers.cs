using EmailRegistration.Data;
using EmailRegistration.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmailRegistration.Controllers
{
    public class FeedControllers : Controller
    {
        [ApiController]
        [Route("api/emails")]
        public class EmailController : ControllerBase
        {
            private readonly AppEmailDbContext _AppEmaildbContext;

            public EmailController(AppEmailDbContext appEmaildbContext)
            {
                _AppEmaildbContext = appEmaildbContext;
            }

            [HttpPost]
            [Route("register")]
            public async Task<IActionResult> RegisterEmail([FromBody] string email)
            {
                if (string.IsNullOrEmpty(email) || !_IsValidEmail(email))
                {
                    return BadRequest("E-mail inválido");
                }
                try
                {
                    var existingEmail = await _AppEmaildbContext.Feed.FirstOrDefaultAsync(e => e.Email.ToLower().Trim() == email.ToLower().Trim());

                    if (existingEmail != null)
                    {
                        return BadRequest("E-mail já registrado");
                    }

                    var newFeed = new Feed
                    {
                        Email = email
                    };

                    object value = _AppEmaildbContext.Feed.Add(newFeed);
                    await _AppEmaildbContext.SaveChangesAsync();

                    return Ok("E-mail registrado com sucesso");
                }
                catch (Exception ex)
                {

                    return StatusCode(500, "Erro interno do servidor");
                }
            }

            private bool _IsValidEmail(string email)
            {
                throw new NotImplementedException();
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetEmail(int id)
            {
                try
                {
                    var usuario = await _AppEmaildbContext.Feed.FindAsync(id);

                    if (usuario == null)
                    {
                        return NotFound("E-mail não encontrado");
                    }

                    return Ok(usuario.Email);
                }
                catch (Exception ex)
                {

                    return StatusCode(500, "Erro interno do servidor");
                }
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteEmail(int id)
            {
                try
                {
                    var Feed = await _AppEmaildbContext.Feed.FindAsync(id);

                    if (Feed == null)
                    {
                        return NotFound("E-mail não encontrado");
                    }

                    _AppEmaildbContext.Feed.Remove(Feed);
                    await _AppEmaildbContext.SaveChangesAsync();

                    return Ok("E-mail excluído com sucesso");
                }
                catch (Exception ex)
                {

                    return StatusCode(500, "Erro interno do servidor");
                }



            }
        }
    }
}
