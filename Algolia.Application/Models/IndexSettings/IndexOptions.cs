using System.Collections.Generic;
using Algolia.Application.Enums;

namespace Algolia.Application.Models.IndexSettings
{
    public class IndexOptions
    {
        public List<CustomRankingAttribute> CustomRankingAttributes { get; set; }
        public List<SearchableAttribute> SearchableAttributes { get; set; }
    }
}