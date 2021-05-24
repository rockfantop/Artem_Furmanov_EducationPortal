using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Interfaces
{
    public interface IServiceResult
    {
        bool IsSuccesful { get; }

        string Message { get; }
    }

    public interface IServiceResult<TResult> : IServiceResult
    {
        TResult Result { get; }
    }
}
