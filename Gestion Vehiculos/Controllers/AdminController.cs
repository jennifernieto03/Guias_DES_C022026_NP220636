using Gestion_Vehiculos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Vehiculos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // Solo accesible por administradores
    public class AdminController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("asignar-rol")]
        public async Task<IActionResult> AsignarRol(string email, string rol)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            if (usuario == null)
                return NotFound("El usuario no fue encontrado.");

            if (!await _roleManager.RoleExistsAsync(rol))
                return BadRequest($"El rol '{rol}' no existe.");

            var result = await _userManager.AddToRoleAsync(usuario, rol);
            if (result.Succeeded)
                return Ok($"Rol '{rol}' asignado exitosamente al usuario {email}.");

            return BadRequest(result.Errors);
        }
    }
}