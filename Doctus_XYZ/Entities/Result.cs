namespace Entities
{
    public class Result<T>
    {
        public Result() { }

        public Result(T data, string message)
        {
            Data = data;
            Message = message;
        }
        public Result(T data, string message, int statusCode)
        {
            Data = data;
            Message = message;
            StatusCode = statusCode;
        }

        public T Data
        {
            get;
            private set;
        }
        public string Message
        {
            get;
            set;
        }
        public int StatusCode
        {
            get;
            set;
        }
    }
}
