using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Employee_Management_System.Common;
public class Result<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public Result(bool isSuccess, string message, T? data = default)
    {
        IsSuccess = isSuccess;
        Message = message;
        Data = data;
    }
    public static Result<T> Success(string message, T? data = default)
    {
        return new Result<T>(true, message, data);
    }
    public static Result<T> Failure(string message)
    {
        return new Result<T>(false, message);
    }
    public override string ToString()
    {
        return IsSuccess ? $"OK: {Message}" : $"FAIL: {Message}";
    }
}
