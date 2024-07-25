namespace MeusMedicamentos.API.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public ApiResponse()
        {
            Errors = new List<string>();
        }

        public ApiResponse(T data)
        {
            Success = true;
            Data = data;
            Errors = new List<string>();
        }

        public ApiResponse(List<string> errors)
        {
            Success = false;
            Errors = errors;
        }

        public ApiResponse(string error)
        {
            Success = false;
            Errors = new List<string> { error };
        }
    }
}
