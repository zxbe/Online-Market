namespace OnlineMarket.Shared.Results
{
    public class CreateResult<T> : BaseResult
    {
        public T CreatedObject { get; set; }
    }
}
