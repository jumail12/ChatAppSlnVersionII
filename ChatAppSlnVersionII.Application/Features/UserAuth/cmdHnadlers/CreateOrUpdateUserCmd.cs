using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.UserAuth.cmdHnadlers
{
    public class CreateOrUpdateUserCmd : IRequest<IApiResult>
    {
        public string? h_user_id { get; set; }
        public string? h_user_name { get; set; }
        public string? h_user_email { get; set; }
        public string? h_user_password { get; set; }
    }

    public class CreateOrUpdateUserCmdHandler : IRequestHandler<CreateOrUpdateUserCmd, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public CreateOrUpdateUserCmdHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IApiResult> Handle(CreateOrUpdateUserCmd request, CancellationToken cancellationToken)
        {
            var para= new DynamicParameters();
            para.Add("@p_h_user_id", request.h_user_id);
            para.Add("@p_h_user_name", request.h_user_name);
            para.Add("@p_h_user_email", request.h_user_email);
            para.Add("@p_h_user_password", request.h_user_password);
            para.Add("@p_user", request.h_user_name);
            var psql= "select createorupdate_userregister(@p_h_user_id,@p_h_user_name,@p_h_user_email,@p_h_user_password,@p_user);";
            var result = await _dataAccess.ExecuteScalarAsync<string>(psql, para, false);
            return new SucessResult<string>(result.ToString())
            {
                ResultType = ResultType.Success,
                Message = "User created/updated successfully",
                Data = result.ToString(),
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
