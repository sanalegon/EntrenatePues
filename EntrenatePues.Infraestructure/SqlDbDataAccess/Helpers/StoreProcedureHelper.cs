using Dapper;
using EntrenatePues.Core.Interfaces.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EntrenatePues.Infraestructure.SqlDbDataAccess.Helpers
{
    public class StoreProcedureHelper : IStoreProcedureHelper
    {
        public IEnumerable<T> ExecuteSp<T, P>(string query, P parameters, string connection)
        {
            IEnumerable<T> result;
            using (SqlConnection cnx = new(connection))
            {
                result = cnx.Query<T>(query, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 9000);
            }
            return result;
        }

        public Task<IEnumerable<T>> ExecuteSpAsync<T, P>(string query, P parameters, string connection)
        {
            Task<IEnumerable<T>> result;
            using (SqlConnection cnx = new(connection))
            {
                result = cnx.QueryAsync<T>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public IEnumerable<T> ExecuteSp<T>(string query, string connection)
        {
            IEnumerable<T> result;
            using (SqlConnection cnx = new(connection))
            {
                result = cnx.Query<T>(query, commandType: CommandType.StoredProcedure);
            }
            return result;
        }
        public Task<IEnumerable<T>> ExecuteSpAsync<T>(string query, string connection)
        {
            Task<IEnumerable<T>> result;
            using (SqlConnection cnx = new(connection))
            {
                result = cnx.QueryAsync<T>(query, commandType: CommandType.StoredProcedure);
            }
            return result;
        }
    }
}
