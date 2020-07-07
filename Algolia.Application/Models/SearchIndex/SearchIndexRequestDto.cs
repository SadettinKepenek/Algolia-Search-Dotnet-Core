using Algolia.Search.Models.Search;

namespace Algolia.Application.Models.CreateIndex
{
    public class SearchIndexRequestDto
    {
        public string IndexName { get; set; }
        public string searchText { get; set; }
        public int MaxResultCount { get; set; }
        public int SkipCount { get; set; }
        public Query Query { get; set; }
    }
}