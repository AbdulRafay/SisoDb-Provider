using System;
using System.Collections.Generic;
using System.Data;
using PineCone.Structures;
using PineCone.Structures.Schemas;
using SisoDb.Providers;
using SisoDb.Querying.Sql;

namespace SisoDb.Dac
{
    public interface IDbClient : IDisposable
    {
        bool IsTransactional { get; }
        ISqlStatements SqlStatements { get; }

        void Flush();
        
        IDbCommand CreateCommand(string sql, params IDacParameter[] parameters);
        IDbCommand CreateSpCommand(string sql, params IDacParameter[] parameters);
        void ExecuteNonQuery(string sql, params IDacParameter[] parameters);
        IDbBulkCopy GetBulkCopy();

        void Drop(IStructureSchema structureSchema);
        void RefreshIndexes(IStructureSchema structureSchema);
        void DeleteById(IStructureId structureId, IStructureSchema structureSchema);
        void DeleteByIds(IEnumerable<IStructureId> ids, IStructureSchema structureSchema);
        void DeleteByQuery(SqlQuery query, IStructureSchema structureSchema);
        void DeleteWhereIdIsBetween(IStructureId structureIdFrom, IStructureId structureIdTo, IStructureSchema structureSchema);
        bool TableExists(string name);
        int RowCount(IStructureSchema structureSchema);
        int RowCountByQuery(IStructureSchema structureSchema, SqlQuery query);
        long CheckOutAndGetNextIdentity(string entityHash, int numOfIds);
        IEnumerable<string> GetJson(IStructureSchema structureSchema);
        string GetJsonById(IStructureId structureId, IStructureSchema structureSchema);
        IEnumerable<string> GetJsonByIds(IEnumerable<IStructureId> ids, IStructureSchema structureSchema);
        IEnumerable<string> GetJsonWhereIdIsBetween(IStructureId structureIdFrom, IStructureId structureIdTo, IStructureSchema structureSchema);

        void SingleResultSequentialReader(string sql, Action<IDataRecord> callback, params IDacParameter[] parameters);
    }
}