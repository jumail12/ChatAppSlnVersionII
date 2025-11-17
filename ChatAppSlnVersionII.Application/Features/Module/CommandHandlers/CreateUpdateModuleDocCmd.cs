using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.Module.CommandHandlers
{
    public class CreateUpdateModuleDocCmd : IRequest<IApiResult>
    {
        public string docId { get; set; }
        public string docMod { get; set; } = string.Empty;          // Module Name (e.g., "User", "Auth")
        public string docPrefix { get; set; } = string.Empty;       // Prefix for IDs (e.g., "US")
        public string? docDescription { get; set; }                 // Optional description
        public int docStartNumber { get; set; } = 1;                // Starting number for ID generation
        public int docLength { get; set; } = 5;                     // Number of digits (e.g., 5 -> 00001)
        public int docCurrentNumber { get; set; } = 0;              // Last used number tracker
        public string? docCreatedBy { get; set; }
    }

    public class CreateUpdateModuleDocHandler : IRequestHandler<CreateUpdateModuleDocCmd, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public CreateUpdateModuleDocHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IApiResult> Handle(CreateUpdateModuleDocCmd request, CancellationToken cancellationToken)
        {
            var param = new DynamicParameters();
            Guid? docId = string.IsNullOrWhiteSpace(request.docId) ? (Guid?)null : Guid.Parse(request.docId);
            param.Add("@p_docId", docId, DbType.Guid);
            param.Add("@p_docMod", request.docMod);
            param.Add("@p_docPrefix", request.docPrefix);
            param.Add("@p_docDescription", request.docDescription);
            param.Add("@p_docStartNumber", request.docStartNumber);
            param.Add("@p_docLength", request.docLength);
            param.Add("@p_docCurrentNumber", request.docCurrentNumber);
            param.Add("@p_user", request.docCreatedBy);
            param.Add("@p_outDocId", dbType: DbType.Guid, direction: ParameterDirection.Output);
            await _dataAccess.ExecuteAsync("CreateOrUpdateModuleDoc", param, true);

            var outDocId = param.Get<Guid>("@p_outDocId");

            return new SucessResult<string>(outDocId.ToString())
            {
                Data = outDocId.ToString(),
                Message = "Success",
                ResultType = ResultType.Success,
            };
        }

    }

}
