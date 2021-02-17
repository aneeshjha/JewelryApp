using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Service
{
    public class RepositoryResult<T>
    {
        public T ResultObject { get; set; }
        public string ErrorMessage { get; set; }
        public HttpStatusCode ErrorCode { get; set; }
        public bool IsError { get; set; }
        public RepositoryResult()
        {
        }

        public static RepositoryResult<T> Create(T resultObject, string errorMessage, HttpStatusCode errorCode)
        {
            return new RepositoryResult<T>
            {
                ResultObject = resultObject,
                ErrorMessage = errorMessage,
                ErrorCode = errorCode,
                IsError = !string.IsNullOrEmpty(errorMessage)
            };
        }
    }
}
