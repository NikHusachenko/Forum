namespace Forum.Services.Response
{
    public class ServiceResponse
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }

        public static ServiceResponse Error(string errorMessage)
        {
            return new ServiceResponse()
            {
                ErrorMessage = errorMessage,
                IsError = true,
            };
        }

        public static ServiceResponse Ok()
        {
            return new ServiceResponse()
            {
                IsError = false,
            };
        }
    }

    public class ServiceResponse<T>
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public T Value { get; set; }

        public static ServiceResponse<T> Error(string errorMessage)
        {
            return new ServiceResponse<T>()
            {
                ErrorMessage = errorMessage,
                IsError = true,
            };
        }

        public static ServiceResponse<T> Ok(T value)
        {
            return new ServiceResponse<T>()
            {
                IsError = false,
                Value = value,
            };
        }
    }
}