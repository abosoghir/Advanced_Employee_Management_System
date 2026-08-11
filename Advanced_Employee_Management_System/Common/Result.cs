using System;
using System.Collections;
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
        if (!IsSuccess)
            return $"FAIL: {Message}";

        return $"OK: {Message}\nData:\n{FormatData(Data)}";
    }

    private static string FormatData(object? data)
    {
        if (data is null)
            return "No data.";

        // Dictionary
        if (data is IDictionary dictionary)
        {
            var sb = new StringBuilder();

            foreach (DictionaryEntry item in dictionary)
            {
                sb.AppendLine($"{item.Key} : {item.Value}");
            }

            return sb.ToString();
        }

        // List, Queue, Stack, HashSet, etc.
        if (data is IEnumerable collection && data is not string)
        {
            var sb = new StringBuilder();

            foreach (var item in collection)
            {
                sb.AppendLine(item?.ToString());
            }

            return sb.ToString();
        }

        // Normal objects
        return data.ToString() ?? string.Empty;
    }
}
