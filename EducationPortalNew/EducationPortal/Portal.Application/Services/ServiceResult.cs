using Portal.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Services
{
    public class ServiceResult : IServiceResult
    {
        public bool IsSuccesful { get; set; }

        public string Message { get; set; }


        public static ServiceResult FromResult(
            bool isSuccessful,
            string resultMessage = null)
        {
            return new ServiceResult()
            {
                IsSuccesful = isSuccessful,
                Message = resultMessage
            };
        }
    }

    public class ServiceResult<TResult> : ServiceResult, IServiceResult<TResult>
    {
        public TResult Result { get; set; }

        public static ServiceResult<TResult> FromResult(
            bool isSuccessful,
            TResult entity,
            string resultMessage = null)
        {
            return new ServiceResult<TResult>()
            {
                IsSuccesful = isSuccessful,
                Result = entity,
                Message = resultMessage
            };
        }
    }
}
