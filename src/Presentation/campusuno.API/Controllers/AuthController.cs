using campusuno.API.DTOs;
using campusuno.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace campusuno.API.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        [EndpointSummary("Register a new user")]
        [EndpointDescription("Register a new user and return a response")]
        [ProducesResponseType(typeof(AuthenticationResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthenticationResponseDTO>> Register([FromBody] RegisterRequestDTO request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser is not null)
            {
                ModelState.AddModelError(nameof(request.Email), "User with this email already exists.");
                return ValidationProblem();
            }

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                Name = request.Name,
                LastName = request.LastName
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                var authResponse = await GenerateToken(request);
                return authResponse;
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return ValidationProblem();
        }

        [HttpPost("login")]
        [EndpointSummary("Login a user")]
        [EndpointDescription("Login a user and return a response")]
        [ProducesResponseType(typeof(AuthenticationResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthenticationResponseDTO>> Login([FromBody] LoginRequestDTO request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                ModelState.AddModelError(nameof(request.Email), "User with this email does not exist.");
                return ValidationProblem();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
                var authResponse = await GenerateToken(new RegisterRequestDTO
                {
                    Email = request.Email,
                    Name = user.Name,
                    LastName = user.LastName
                });
                return authResponse;
            }

            ModelState.AddModelError(nameof(request.Password), "Invalid password.");
            return ValidationProblem();
        }

        private async Task<AuthenticationResponseDTO> GenerateToken(RegisterRequestDTO request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var claimsDB = await _userManager.GetClaimsAsync(user!);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user!.Id.ToString()),
                new Claim(ClaimTypes.Email, request.Email),
            };

            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(1);

            var securityToken = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"]!, audience: _configuration["Jwt:Audience"], claims: claims, expires: expiration, signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return new AuthenticationResponseDTO
            {
                Token = token,
                ExpirationDate = expiration
            };
        }
    }
}
