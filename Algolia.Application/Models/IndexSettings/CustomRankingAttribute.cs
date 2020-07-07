using Algolia.Application.Enums;

namespace Algolia.Application.Models.IndexSettings
{
    public class CustomRankingAttribute
    {
        public string Attribute { get; set; }
        public RankingFunctionsEnum RankingType { get; set; }
    }
}