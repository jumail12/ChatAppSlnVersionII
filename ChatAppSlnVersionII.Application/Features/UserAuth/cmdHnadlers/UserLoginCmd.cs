using ChatAppSlnVersionII.Application.Dtos.Previlage;
using ChatAppSlnVersionII.Application.Dtos.UserAuth;
using ChatAppSlnVersionII.Application.Features.Previlage.Mapping.Query;
using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.UserAuth.cmdHnadlers
{
    public class UserLoginCmd() : IRequest<IApiResult>
    {
        public string p_userEmail { get; set; }
        public string p_userPassword { get; set; }
    }

    public class UserLoginCmdHandler : IRequestHandler<UserLoginCmd, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        public UserLoginCmdHandler(IDataAccess dataAccess,IConfiguration configuration,IMediator mediator)
        {
            _dataAccess = dataAccess;
            _configuration = configuration;
            _mediator = mediator;
        }
        public async Task<IApiResult> Handle(UserLoginCmd request, CancellationToken cancellationToken)
        {
            var para = new DynamicParameters();
            para.Add("@p_user_email", request.p_userEmail);
            para.Add("@p_user_password", request.p_userPassword);
            var psql = "select f_user_login(@p_user_email,@p_user_password)";
            var result = await _dataAccess.ExecuteScalarAsync<string>(psql, para, false);

            var jwtKey = _configuration["JwtSettings:Key"];
            int expmin = Convert.ToInt32(_configuration["JwtSettings:ExpiryMinutes"]);


            string accessToken = GenerateJwtToken(jwtKey, result, request.p_userEmail, expmin);
            string refreshToken = GenerateRefreshToken();

            var getAuthbyid=await _mediator.Send(new AuthGetByUserIdQuery(result));
            var authData = new AuthGetByUserId();
            if (getAuthbyid.ResultType == ResultType.Success && getAuthbyid is SucessResult<List<AuthGetByUserId>> ddata)
            {
                authData = ddata.Data.FirstOrDefault() ?? new AuthGetByUserId();
            }

            authData.access_token = accessToken;
            authData.refresh_token = refreshToken;

            var authcmd=new AuthCreateOrUpdateCmd
            {
                token_id = authData.token_id??"",
                user_id = result,
                access_token = authData.access_token,
                refresh_token = authData.refresh_token,
                exp_at = DateTime.UtcNow.AddMinutes(expmin),
                p_user = result
            };
            var tres=await _mediator.Send(authcmd);

            var privilageRes=await _mediator.Send(new GetRolePrivilageByUserIdQuery(result));
            var prvilageData= new List<roleprevilages_byuserid_dto>();
            if (privilageRes.ResultType == ResultType.Success && privilageRes is SucessResult<List<roleprevilages_byuserid_dto>> pdata)
            {
                prvilageData = pdata.Data;
            }

            var loginResponse = new LoginResponseDto
            {
                userId = result,
                accessToken = accessToken,
                refreshToken = refreshToken,
                prvilages = prvilageData,
                email=request.p_userEmail
            };

            return new SucessResult<LoginResponseDto>(loginResponse)
            {
                Message = "User logged in successfully",
                ResultType = ResultType.Success,
                Data = loginResponse,
                StatusCode = StatusCodes.Status200OK
            };
        }

        private string GenerateJwtToken(string key, string userId, string email, int expmin = 20)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
              new Claim(ClaimTypes.NameIdentifier, userId),
              new Claim(ClaimTypes.Email, email),
              new Claim("uid", userId)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expmin),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

    }
}
