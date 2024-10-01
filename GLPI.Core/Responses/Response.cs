using System.Text.Json.Serialization;

namespace GLPI.Core.Responses;
public class Response<TData>
{
    private const int DEFAULT_CODE = 200;

    [JsonConstructor]
    public Response() => _code = Configuration.DefaultStatusCode;
    
    public Response(
        TData? data,
        int code = DEFAULT_CODE,
        string? message = null)
    {
        Data = data;
        _code = code;
        Message = message;
    }
    private readonly int _code;
    public TData? Data { get; set; }
    public string? Message { get; set; }

    [JsonIgnore]
    public bool IsSuccess 
        => _code >= 200 && _code <= 299;
}
