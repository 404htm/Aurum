using System;

namespace Aurum.Core
{
    public interface IDataConnection
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}
