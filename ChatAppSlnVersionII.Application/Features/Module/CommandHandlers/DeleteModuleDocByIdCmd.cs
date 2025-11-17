using ChatAppSlnVersionII.Application.Dtos.ModuleDocDtos;
using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.Module.CommandHandlers
{
    public class DeleteModuleDocByIdCmd : IRequest<IApiResult>
    {
        public Guid Id { get; set; }
    }

    public class DeleteModuleDocByIdCmdHandler: IRequestHandler<DeleteModuleDocByIdCmd, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public DeleteModuleDocByIdCmdHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IApiResult> Handle(DeleteModuleDocByIdCmd request, CancellationToken cancellationToken)
        {
            var para = new DynamicParameters();
            para.Add("@p_docid", request.Id);
            var psql= "select delete_module_doc_by_id(@p_docid);";
            var res = await _dataAccess.ExecuteAsync(psql, para, false);
            return new BaseApiExeResult
            {
                Message="Success",
                ResultType=ResultType.Success
            };
        }
    }
}
