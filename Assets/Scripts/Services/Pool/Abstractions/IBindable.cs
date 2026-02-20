using System;

namespace Services.Pool.Abstractions
{
    public interface IBindable
    {
        Type BindType { get; }
    }
}
