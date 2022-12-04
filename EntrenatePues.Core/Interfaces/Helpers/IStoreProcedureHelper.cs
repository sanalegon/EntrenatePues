using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntrenatePues.Core.Interfaces.Helpers
{
    public interface IStoreProcedureHelper
    {
        IEnumerable<T> ExecuteSp<T, P>(string query, P parameters, string connection);
        Task<IEnumerable<T>> ExecuteSpAsync<T, P>(string query, P parameters, string connection);
        IEnumerable<T> ExecuteSp<T>(string query, string connection);
        Task<IEnumerable<T>> ExecuteSpAsync<T>(string query, string connection);
    }
}
