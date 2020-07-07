using Algolia.Application.Enums;

namespace Algolia.Application.Models.IndexSettings
{
    public class SearchableAttribute
    {
        /// <summary>
        /// Property Name
        /// </summary>
        public string Attribute { get; set; }
        /// <summary>
        /// Searchable attribute function type (ordered,unordered etc.)
        /// </summary>
        public SearchableAttributeFunctionsEnum? SearchableAttributeFunctionType { get; set; }
    }
}