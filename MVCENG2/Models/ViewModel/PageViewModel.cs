namespace HoffmanWebstatistic.Models.ViewModel
{
    public class PageViewModel
    {
        public int PageNumber { get; }
        public int TotalPages { get; }
        public List<int> PagesList { get; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public PageViewModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            PagesList = new List<int>() { 1 };              
            PagesList.AddRange(Enumerable.Range(pageNumber - 2, 5));
            PagesList.Add(TotalPages);
            PagesList = PagesList.Where(k => k > 0 && k <= TotalPages).Distinct().ToList();
        }
    }
}