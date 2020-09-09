using Dapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Location.Api.Application.Commands
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand>
    {
        private const string command = @"UPDATE Address
                                           SET StreetName = @StreetName
                                              ,Number = @Number
                                         WHERE Id = @Id";

        private readonly IDbConnection _dbConnection;
        private readonly IDistributedCache _cache;

        public UpdateAddressCommandHandler(IDbConnection dbConnection,
            IDistributedCache cache)
        {
            _dbConnection = dbConnection;
            _cache = cache;
        }

        public async Task<Unit> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            await UpdateAddress(request);

            await _cache.RemoveAsync($"location:{request.ZipCode}");

            return Unit.Value;
        }

        private async Task UpdateAddress(UpdateAddressCommand request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", request.Id);
            parameters.Add("StreetName", request.StreetName);
            parameters.Add("Number", request.Number);

            await _dbConnection.ExecuteAsync(command, parameters);
        }
    }
}
