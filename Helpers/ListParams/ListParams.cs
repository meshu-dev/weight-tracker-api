namespace WeightTracker.Api.Helpers.ListParams
{
    public class ListParams
    {
        private readonly int MaxCount = 20;

        public int Page { get; set; } = 1;

        private int _count = 10;
        
        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = (value > MaxCount) ? MaxCount : value;
            }
        }

        private string _sort;

        public string Sort
        {
            get
            {
                return _sort;
            }
            set
            {
                string[] orderParams = value.Split("_");
                
                var orderField = orderParams[0];
                orderField = char.ToUpper(orderField[0]) + orderField.Substring(1);

                var orderType = orderParams[1] == "asc" ? "ascending" : "descending";

                _sort = orderField + " " + orderType;
            }
        }
    }
}
