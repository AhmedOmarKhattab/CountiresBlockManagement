namespace BlockCountriesTask.Dtos
{
    public class Pagination
    {
     
        public int PageSize { get; set; }
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public object Items { get; set; }
        public Pagination(int pageSize, int pageIndex, int count, object data)
        {
            PageSize = pageSize;
            Count = count;
            PageIndex = pageIndex;
            Items = data;
        }



    }
}
