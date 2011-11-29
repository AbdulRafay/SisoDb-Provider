﻿using System.Collections.Generic;
using PineCone.Structures.Schemas;
using SisoDb.Dac;

namespace SisoDb.DbSchema
{
    public class DbSchemaManager : IDbSchemaManager
    {
        private readonly ISet<string> _upsertedSchemas;

        public DbSchemaManager()
        {
            _upsertedSchemas = new HashSet<string>();
        }

        public void ClearCache()
        {
            lock (_upsertedSchemas)
            {
                _upsertedSchemas.Clear();
            }
        }

        public void DropStructureSet(IStructureSchema structureSchema, IDbClient dbClient)
        {
            lock (_upsertedSchemas)
            {
                _upsertedSchemas.Remove(structureSchema.Name);

                dbClient.Drop(structureSchema);
            }
        }

        public void UpsertStructureSet(IStructureSchema structureSchema, IDbSchemaUpserter upserter)
        {
            lock (_upsertedSchemas)
            {
                if (_upsertedSchemas.Contains(structureSchema.Name))
                    return;

                upserter.Upsert(structureSchema);

                _upsertedSchemas.Add(structureSchema.Name);
            }
        }
    }
}