using Microsoft.AspNetCore.Mvc;

namespace Server.Services
{
    public class ServiceResponse<T> : ObjectResult
    {
        public ServiceResponse() : base(new()) { }
         public ServiceResponse(T data, int statusCode) : base(data) { }
        public ServiceResponse(string errorMessage, int statusCode) : base(errorMessage) { }


        private T? _data;
        public T? Data
        {
            get => _data;
            set
            {
                Value = value;
                _data = value;
            }
        }
        private string? _errorMessage;
        public string? ErrorMessage
        {
            get => _errorMessage;
            set
            {
                Value = value;
                _errorMessage = value;
            }
        }
    }
}