﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Vizeeta.Domain.Entities;
using Vizeeta.Service.IServices;
using Vizeeta.Service.JwtData;

namespace Vizeeta.Service.Services
{
    public class TokenService:ITokenService
    {
        public async Task<string> CreateToken(AppUser user, UserManager<AppUser> userManager)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, $"{user.FName +" "+ user.LName}"),
            }; // Private Claims (UserDefined)

            var userRoles = await userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));


            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT.Key));

            var token = new JwtSecurityToken(

                issuer: JWT.ValidIssuer,
                audience: JWT.ValidAudience,
                expires: DateTime.Now.AddDays(double.Parse(JWT.DurationInDays)),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
