namespace marketplace.Helpers.Exceptions
{
    public class ErrorResponse<T>
    {
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public T Data { get; set; }

        public ErrorResponse()
        {
            ErrorCode = "0";
            ErrorDescription = "OK";
        }
    }
}
