namespace MeusMedicamentos.Shared
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public T? Data { get; set; }
        public List<string> Errors { get; set; }

        public ApiResponse()
        {
            Errors = new List<string>();
        }

        public ApiResponse(T data, int statusCode = 200)
        {
            Success = true;
            Data = data;
            Errors = new List<string>();
            StatusCode = statusCode;
        }

        public ApiResponse(List<string> errors, int statusCode = 400)
        {
            Success = false;
            Data = default;
            Errors = errors;
            StatusCode = statusCode;
        }

        public ApiResponse(string error, int statusCode = 400)
        {
            Success = false;
            Data = default;
            Errors = new List<string> { error };
            StatusCode = statusCode;
        }

        public ApiResponse(int statusCode)
        {
            Success = false;
            Data = default;
            Errors = new List<string>();
            StatusCode = statusCode;
        }
    }
}
