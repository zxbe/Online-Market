namespace OnlineMarket.Shared.Results
{
    using System.Collections.Generic;

    public class BaseResult
    {
        public bool IsSuccessful { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
