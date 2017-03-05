using System;

namespace Aurum.Core
{
    public interface IDataSource
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}
