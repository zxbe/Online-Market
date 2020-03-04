namespace OnlineMarket.Shared.Results
{
    public class GetResult<T> : BaseResult
    {
        public T Object { get; set; }
    }
}
