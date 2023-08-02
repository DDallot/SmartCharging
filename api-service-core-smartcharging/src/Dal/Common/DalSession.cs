using System.Data.SqlClient;
using System.Data;

namespace Api.Services.Core.SmartCharging.Dal.Common
{
    public sealed class DalSession: IDalSession
    {
        private readonly string _connectionString;
        private readonly IDbConnection _connection;
        UnitOfWork? _unitOfWork = null;

        public DalSession(IConfiguration configuration)
        {
            _connectionString = configuration?.GetConnectionString("SmartCharging") ?? throw new ArgumentNullException(nameof(configuration));
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            _unitOfWork = new UnitOfWork(_connection);
        }

        public UnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
            _connection.Dispose();
        }
    }
}
