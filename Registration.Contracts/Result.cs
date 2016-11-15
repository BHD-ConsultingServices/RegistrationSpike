
namespace Registration.Contracts
{
    public class Result<T>
    {
        public T Data { get; set; }

        public ResultCode ResultCode { get; set; }
    }
}
