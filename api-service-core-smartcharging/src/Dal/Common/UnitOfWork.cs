using System.Data;

namespace Api.Services.Core.SmartCharging.Dal.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection = null;
        private IDbTransaction _transaction = null;
        private readonly Guid _id = Guid.Empty;
        public IDbConnection Connection
        {
            get { return _connection; }
        }
        public IDbTransaction Transaction
        {
            get { return _transaction; }
        }
        public Guid Id
        {
            get { return _id; }
        }

        public UnitOfWork() { }

        internal UnitOfWork(IDbConnection connection)
        {
            _id = Guid.NewGuid();
            _connection = connection;
        }
        
        public void Begin()
        {
            _transaction = _connection.BeginTransaction(IsolationLevel.Serializable);
        }

        public void Commit()
        {
            _transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();
            _transaction = null;
        }
    }
}
