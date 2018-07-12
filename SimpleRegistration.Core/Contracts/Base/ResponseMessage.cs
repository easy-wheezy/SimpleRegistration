namespace SimpleRegistration.Core.Contracts.Base
{
    public class ResponseMessage
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }

        protected ResponseMessage(bool success, string message = null)
        {
            Success = success;
            Message = message;
        }
    }
}
