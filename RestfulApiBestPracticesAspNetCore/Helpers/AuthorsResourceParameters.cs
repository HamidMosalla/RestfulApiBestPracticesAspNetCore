namespace AspNetCoreWebApiSamples.Helpers
{
    public class AuthorsResourceParameters
    {
        const int maxPageSize = 20;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public string Genre { get; set; }

        public string SearchQuery { get; set; }

        public string OrderBy { get; set; } = "Name";

        public string Fields { get; set; }
    }
}
