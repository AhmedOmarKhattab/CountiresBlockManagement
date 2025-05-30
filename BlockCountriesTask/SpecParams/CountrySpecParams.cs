namespace BlockCountriesTask.SpecParams
{
    public class CountrySpecParams
    {
        private const int MaxSize = 10;
        private int pagesize = 5;
        public int PageIndex { set; get; } = 1;
        public int PageSize
        {
            get { return pagesize; }
            set { pagesize = value > MaxSize ? MaxSize : value; }
        }
        public string? Search {  get; set; }
    }
}
