using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public UserLoginCmdHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IApiResult> Handle(UserLoginCmd request, CancellationToken cancellationToken)
        {
            var para = new DynamicParameters();
            para.Add("@p_user_email", request.p_userEmail);
            para.Add("@p_user_password", request.p_userPassword);
            var psql = "select f_user_login(@p_user_email,@p_user_password)";
            var result =await _dataAccess.ExecuteScalarAsync<string>(psql, para, false);

            return new SucessResult<string>(result.ToString())
            {
                Message="User logged in successfully",
                ResultType=ResultType.Success,
                Data=result.ToString()
            };
        }
    }
}
