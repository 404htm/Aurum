using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Aurum.SQL
{
    public interface ISqlValidator : IDisposable
    {
        bool Validate(string query, out IList<SqlError> errors);
    }
}