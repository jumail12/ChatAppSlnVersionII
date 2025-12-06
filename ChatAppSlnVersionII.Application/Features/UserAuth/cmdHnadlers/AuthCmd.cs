using ChatAppSlnVersionII.Application.Dtos.UserAuth;
using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.UserAuth.cmdHnadlers
{
    public class AuthCreateOrUpdateCmd: IRequest<IApiResult>
    {
        public string token_id { get; set; }
        public string user_id { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public DateTime exp_at { get; set; }
        public string? p_user { get; set; }
    }
    public class AuthCreateOrUpdateCmdHandler : IRequestHandler<AuthCreateOrUpdateCmd, IApiResult>
    {
        private readonly IDataAccess _dataAccess;

        public AuthCreateOrUpdateCmdHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IApiResult> Handle(AuthCreateOrUpdateCmd request, CancellationToken cancellationToken)
        {
            var pparams = new Dapper.DynamicParameters();
            pparams.Add("@p_token_id", request.token_id);
            pparams.Add("@p_user_id", request.user_id);
            pparams.Add("@p_access_token", request.access_token);
            pparams.Add("@p_refresh_token", request.refresh_token);
            pparams.Add("@p_exp_at", request.exp_at);
            pparams.Add("@p_user", request.p_user);

            string psql = "select create_or_update_auth(@p_token_id,@p_user_id,@p_access_token,@p_refresh_token,@p_exp_at,@p_user);";
            var res=await _dataAccess.ExecuteScalarAsync<string>(psql, pparams, false);
            return new SucessResult<string>(res)
            {
                ResultType = ResultType.Success,
                Message = "User authentication record created/updated successfully.",
                Data= res,
                StatusCode = StatusCodes.Status200OK
            };
        }
    }



    //get auth by user  id
    public record AuthGetByUserIdQuery(string? userId) : IRequest<IApiResult>;
    public class AuthGetByUserIdQueryHandler : IRequestHandler<AuthGetByUserIdQuery, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public AuthGetByUserIdQueryHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IApiResult> Handle(AuthGetByUserIdQuery request, CancellationToken cancellationToken)
        {
            var pparams = new Dapper.DynamicParameters();
            pparams.Add("@p_user_id", request.userId);
            string psql = "select * from get_auth_by_userid(@p_user_id);";
            var res = await _dataAccess.ExecuteListAsync<AuthGetByUserId>(psql, pparams, false);
            return new SucessResult<List<AuthGetByUserId>>(res)
            {
                ResultType =res.Count>0? ResultType.Success : ResultType.NoData,
                Message = res.Count > 0 ? "User authentication records fetched successfully.":"No Data",
                Data = res,
                StatusCode = res.Count > 0 ? StatusCodes.Status200OK : StatusCodes.Status204NoContent


            };
        }
    }



    //get auth by refresh token
    public record AuthGetByRefrshTokenQuery(string? refreshtoken) : IRequest<IApiResult>;
    public class AuthGetByRefrshTokenQueryHandler : IRequestHandler<AuthGetByRefrshTokenQuery, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public AuthGetByRefrshTokenQueryHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IApiResult> Handle(AuthGetByRefrshTokenQuery request, CancellationToken cancellationToken)
        {
            var pparams = new Dapper.DynamicParameters();
            pparams.Add("@p_refresh_token", request.refreshtoken);
            string psql = "select * from get_auth_by_refreshtoken(@p_refresh_token);";
            var res = await _dataAccess.ExecuteListAsync<AuthGetByUserId>(psql, pparams, false);
            return new SucessResult<List<AuthGetByUserId>>(res)
            {
                ResultType =res.Count>0? ResultType.Success : ResultType.NoData,
                Message = res.Count > 0 ? "Success." : "No Data",
                Data = res,
                StatusCode = res.Count > 0 ? StatusCodes.Status200OK : StatusCodes.Status204NoContent
            };
        }
    }

    public record AuthGetByListQuery() : IRequest<IApiResult>
    {
        public string? token_id { get; set; }
        public string? p_search { get; set; }
    }
    public class AuthGetByListQueryQueryHandler : IRequestHandler<AuthGetByListQuery, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public AuthGetByListQueryQueryHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IApiResult> Handle(AuthGetByListQuery request, CancellationToken cancellationToken)
        {
            var pparams = new Dapper.DynamicParameters();
            pparams.Add("@p_token_id", request.token_id);
            pparams.Add("@p_search", request.p_search);
            string psql = "select * from get_all_auth(@p_token_id,@p_search);";
            var res = await _dataAccess.ExecuteListAsync<AuthGetByUserId>(psql, pparams, false);
            return new SucessResult<List<AuthGetByUserId>>(res)
            {
                ResultType = res.Count > 0 ? ResultType.Success : ResultType.NoData,
                Message = res.Count > 0 ? "Success.": "No Data",
                Data = res,
                StatusCode = res.Count > 0 ? StatusCodes.Status200OK : StatusCodes.Status204NoContent
            };
        }
    }



    //delete
    public class AuthDeleteByUserIdCmd() : IRequest<IApiResult>
    {
        public string? token_id { get; set; } 
        public string? p_user { get; set; }
    }

    public class AuthDeleteByUserIdCmdHandler : IRequestHandler<AuthDeleteByUserIdCmd, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public AuthDeleteByUserIdCmdHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IApiResult> Handle(AuthDeleteByUserIdCmd request, CancellationToken cancellationToken)
        {
            var pparams = new Dapper.DynamicParameters();
            pparams.Add("@p_token_id", request.token_id);
            pparams.Add("@p_user", request.p_user);
            string psql = "select delete_auth_by_token_id(@p_token_id,@p_user);";
            var res = await _dataAccess.ExecuteAsync(psql, pparams, false);
            return new BaseApiExeResult
            {
                ResultType = ResultType.Success,
                Message = "User authentication record deleted successfully.",
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
