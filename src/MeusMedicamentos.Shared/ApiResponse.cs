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
            StatusCode = statusCode;
            Data = data;
            Errors = new List<string>();
        }

        public ApiResponse(List<string> errors, int statusCode = 400)
        {
            Success = false;
            StatusCode = statusCode;
            Data = default;
            Errors = errors;
        }

        public ApiResponse(string error, int statusCode = 400)
        {
            Success = false;
            StatusCode = statusCode;
            Data = default;
            Errors = new List<string> { error };
        }

        public ApiResponse(int statusCode)
        {
            Success = false;
            StatusCode = statusCode;
            Data = default;
            Errors = new List<string>();
        }
    }
}
