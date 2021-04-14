using MisGastos.Core.Entities;
using System.Collections.Generic;

namespace MisGastos.Api.Response
{
    public class ApiResponse<T>
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public List<string> errors { get; set; }
        public T data { get; set; }
        public Metadata meta { get; set; }
        public ApiResponse()
        {

        }
        public ApiResponse(T _data)
        {
            isSuccess = true;
            data = _data;
        }
        public ApiResponse(T _data, Metadata metadata)
        {
            isSuccess = true;
            data = _data;
            meta = metadata;
        }
        public ApiResponse(T _data, string _message = null)
        {
            isSuccess = true;
            message = _message;
            data = _data;
        }
        public ApiResponse(string _message)
        {
            isSuccess = false;
            message = _message;
        }

    }
}
