﻿using InventoryManagementSystem.App.Entities;
using InventoryManagementSystem.App.Extensions;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Repository;
using InventoryManagementSystem.App.Response;
using InventoryManagementSystem.App.Response.RequestResult;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventoryManagementSystem.App.Features.Auth.Login.Command
{
    public record LoginCommand(string Email, string Password) : IRequest<RequestResult<AuthModel>>;


    public class AuthanticationHandler : BaseRequestHandler<LoginCommand, RequestResult<AuthModel>>
    {
        private IUnitOfWork _unitofwork;

        private JWT jwt;
        public AuthanticationHandler(IUnitOfWork unitOfWork, IOptions<JWT> jwt, BaseRequestHandlerParam param) : base(param)
        {
            _unitofwork = unitOfWork;
            this.jwt = jwt.Value;
        }

        public override async Task<RequestResult<AuthModel>> Handle(LoginCommand loginCommand, CancellationToken token)
        {
            var authModel = new AuthModel();


            var user = await _unitofwork.GetRepository<User>().GetAll(e => e.Email == loginCommand.Email).FirstOrDefaultAsync();

            if (user == null)
            {
                return RequestResult<AuthModel>.Failure(ErrorCode.UserNotFound, "User Does not exist");

            }

            var correctPassword = PasswordHasherService.ValidatePassword(loginCommand.Password, user.Password);
            if (correctPassword)
            {
                var userRole = user.Role;
                var claims = GenerateClaims(user);
                var jwtSecurityToken = CreateAccessToken(claims);
                authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authModel.ExpiresOn = jwtSecurityToken.ValidTo;
                authModel.IsAuthenticated = true;
                authModel.Email = user.Email;
                authModel.UserName = user.Username;

                return RequestResult<AuthModel>.Success(authModel, "login succeeded");

            }
            return RequestResult<AuthModel>.Failure(ErrorCode.IncorrectPassword, "User Does not exist");

        }


        private static Claim[] GenerateClaims(User user)
        {
            var identifier = user.Id.ToString();

            return
            [
                new Claim(ClaimTypes.NameIdentifier, identifier)
,
            new Claim(JwtRegisteredClaimNames.UniqueName, identifier),
            new Claim(JwtRegisteredClaimNames.Sub, user.Username, ClaimValueTypes.String),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role,user.Role.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
            ];
        }

        private JwtSecurityToken CreateAccessToken(Claim[] claims)
        {
            var createdAt = DateTime.UtcNow;

            var expiresAt = createdAt.AddDays(jwt.DurationInDays);

            var keyBytes = Encoding.UTF8.GetBytes(jwt.Key);

            var symmetricSecurityKey = new SymmetricSecurityKey(keyBytes);

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
