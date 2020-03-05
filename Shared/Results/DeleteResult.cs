namespace OnlineMarket.Shared.Results
{
    public class DeleteResult<T> : BaseResult
    {
        public T DeletedObject { get; set; }
    }
}