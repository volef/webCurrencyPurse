using System.Text.Json.Serialization;
using webTest.Serialization;

namespace webTest.Models
{
    public enum ResultEnum
    {
        Success,
        Fail,
        Error
    }

    public class Result<T>
    {
        public ResultEnum Status { get; }
        public T Value { get; }

        public string Description { get; }

        public Result(ResultEnum status, T value, string description)
        {
            Status = status;

            Value = value;

            if (Status == ResultEnum.Success)
                Description = "Success";
            else
                Description = description;
        }

        public Result GetWithoutValue()
        {
            return new Result(Status, Description);
        }
    }

    public class Result
    {
        public ResultEnum Status { get; }

        public string Description { get; }

        public Result(ResultEnum status, string description)
        {
            Status = status;

            if (Status == ResultEnum.Success)
                Description = "Success";
            else
                Description = description;
        }
    }
}