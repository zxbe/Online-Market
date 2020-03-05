namespace OnlineMarket.Shared.Results
{
    public class UpdateResult<T> : BaseResult
    {
        public T PreviousObject { get; set; }
        public T UpdatedObject { get; set; }
    }
}
