using Microsoft.AspNetCore.Mvc;

namespace Server.Services
{
    public class ServiceResponse<T> : ObjectResult
    {
        public ServiceResponse() : base(new()) { }


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