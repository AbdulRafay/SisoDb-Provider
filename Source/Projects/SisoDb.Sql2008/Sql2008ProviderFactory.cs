﻿using PineCone.Structures.Schemas;
using SisoDb.Dac;
using SisoDb.Dac.BulkInserts;
using SisoDb.DbSchema;
using SisoDb.Querying;
using SisoDb.Querying.Lambdas.Parsers;
using SisoDb.Sql2008.Dac;
using SisoDb.Structures;

namespace SisoDb.Sql2008
{
	public class Sql2008ProviderFactory : IDbProviderFactory
    {
		private readonly IConnectionManager _connectionManager;
        private readonly ISqlStatements _sqlStatements;
		
        public Sql2008ProviderFactory()
        {
			_connectionManager = new Sql2008ConnectionManager();
            _sqlStatements = new Sql2008Statements();
        }

        public StorageProviders ProviderType
        {
            get { return StorageProviders.Sql2008; }
        }

        public IConnectionManager ConnectionManager
        {
            get { return _connectionManager; }
        }

		public ISqlStatements GetSqlStatements()
		{
			return _sqlStatements;
		}

		public virtual IServerClient GetServerClient(ISisoConnectionInfo connectionInfo)
        {
            return new Sql2008ServerClient(connectionInfo, _connectionManager, _sqlStatements);
        }

	    public ISisoTransaction GetRequiredTransaction()
	    {
	        return Sql2008DbTransaction.CreateRequired();
	    }

	    public ISisoTransaction GetSuppressedTransaction()
	    {
            return Sql2008DbTransaction.CreateSuppressed();
	    }

        public ITransactionalDbClient GetTransactionalDbClient(ISisoConnectionInfo connectionInfo)
        {
            var transaction = GetRequiredTransaction();

            return new Sql2008DbClient(
                connectionInfo,
                _connectionManager.OpenClientDbConnection(connectionInfo),
                transaction,
                _connectionManager,
                _sqlStatements);
        }

	    public IDbClient GetNonTransactionalDbClient(ISisoConnectionInfo connectionInfo)
	    {
	        var transaction = GetSuppressedTransaction();

	        return new Sql2008DbClient(
                connectionInfo,
                _connectionManager.OpenClientDbConnection(connectionInfo),
                transaction,
                _connectionManager,
                _sqlStatements);
	    }

	    public virtual IDbSchemaManager GetDbSchemaManager()
        {
			return new DbSchemaManager(new SqlDbSchemaUpserter(_sqlStatements));
        }

        public virtual IStructureInserter GetStructureInserter(IDbClient dbClient)
        {
            return new DbStructureInserter(dbClient);
        }

    	public IIdentityStructureIdGenerator GetIdentityStructureIdGenerator(CheckOutAngGetNextIdentity action)
    	{
    		return new DbIdentityStructureIdGenerator(action);
    	}

    	public virtual IDbQueryGenerator GetDbQueryGenerator()
        {
            return new Sql2008QueryGenerator(_sqlStatements);
        }

    	public IQueryBuilder<T> GetQueryBuilder<T>(IStructureSchemas structureSchemas) where T : class
    	{
    		return new QueryBuilder<T>(structureSchemas, new ExpressionParsers());
    	}
    }
}