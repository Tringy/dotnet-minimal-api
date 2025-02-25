
using System.Security.Claims;
using Application.Users.GetById;
using Application.Users.Login;
using Application.Users.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Infrastructure;


namespace Web.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        /// <summary>
        /// Welcome a user by their username using jwt token.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing the user information if found, otherwise an error.</returns>
        /// <response code="200">Returns the user information.</response>
        /// <response code="404">The user was not found.</response>
        [Authorize]
        [HttpGet("welcome")]
        public async Task<IActionResult> GetByName(CancellationToken cancellationToken)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var query = new GetUserWelcomeMessageQuery(userEmail);

            Result<UserWelcomeResponse> result = await _sender.Send(query, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return CustomResults.Problem(result);
        }
        
        /// <summary>
        /// Logs in a user and returns a JWT token.
        /// </summary>
        /// <param name="request">The login request containing the user's email and password.</param>
        /// <returns>An <see cref="IActionResult"/> containing the login result if successful, otherwise an error.</returns>
        /// <response code="200">Returns the JWT token.</response>
        /// <response code="404">The user was not found</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var command = new LoginUserCommand(request.Email, request.Password);

            Result<string> result = await _sender.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return CustomResults.Problem(result);
        }

        /// <summary>
        /// Registers a new user with email, password and username.
        /// </summary>
        /// <param name="request">The registration request containing the user's email, username, and password.</param>
        /// <returns>An <see cref="IActionResult"/> containing the registration result if successful, otherwise an error.</returns>
        /// <response code="200">Returns the user id.</response>
        /// <response code="400">The request was invalid.</response>
        /// <response code="409">The user already exists.</response>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var command = new RegisterUserCommand(
                request.Email,
                request.UserName,
                request.Password);

            Result<Guid> result = await _sender.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return CustomResults.Problem(result);
        }

        public sealed record LoginRequest(string Email, string Password);
        public sealed record RegisterRequest(string Email, string UserName, string Password);
    }
}
