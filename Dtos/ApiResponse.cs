namespace bsa_api.Dtos;

public class ApiResponse<T>
{
    public bool Status { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }

    public static ApiResponse<T> Ok(T? data, string message = "Success") =>
        new() { Status = true, Message = message, Data = data };

    public static ApiResponse<T> Fail(string message) =>
        new() { Status = false, Message = message, Data = default };
}