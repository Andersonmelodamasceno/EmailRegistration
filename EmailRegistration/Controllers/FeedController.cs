
using EmailRegistration.Data;
using EmailRegistration.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EmailRegistration.Controllers
{
    [Route("api/emails")]
    [ApiController]
    public class FeedController : ControllerBase
    {


        private readonly AppEmailDbContext _AppEmaildbContext;

        public FeedController(AppEmailDbContext appEmaildbContext)
        {
            _AppEmaildbContext = appEmaildbContext;
        }

        /* [HttpPost]
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

 */



            [HttpPost]
            [Route("register")]
            public async Task<IActionResult> RegisterEmail([FromBody] Feed feed)
            {
                if (feed == null || string.IsNullOrEmpty(feed.Email) || string.IsNullOrEmpty(feed.Name) || !_IsValidEmail(feed.Email))
                {
                    return BadRequest("Nome ou e-mail inválido");
                }
                try
                {
                    var existingEmail = await _AppEmaildbContext.Feed.FirstOrDefaultAsync(e => e.Email.ToLower().Trim() == feed.Email.ToLower().Trim());

                    if (existingEmail != null)
                    {
                        return BadRequest("E-mail já registrado");
                    }

                    feed.CreatedAt = DateTime.UtcNow;
                    feed.Removed = false;

                    _AppEmaildbContext.Feed.Add(feed);
                    await _AppEmaildbContext.SaveChangesAsync();

                    return Ok("E-mail e nome registrados com sucesso");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Erro interno do servidor");
                }
            }

            private bool _IsValidEmail(string email)
            {
                // Lógica de validação de e-mail adequada deve ser implementada aqui.
                // A implementação da validação de e-mail pode variar dependendo dos requisitos.
                return !string.IsNullOrEmpty(email) && email.Contains("@");
            }




            [HttpGet("{id}")]
        public async Task<IActionResult> GetEmail(Guid id)
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
        public async Task<IActionResult> DeleteEmail(Guid id)
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
