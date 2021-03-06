﻿using System.Threading.Tasks;

namespace Aurum.Core.Parser
{
    public interface IParser<T>
    {
        Task<T> Parse(string expression);
        Task<T> Parse(string expression, IScope scope);
        void Import(string v);
        void Register<T2>();
    }
}