using Algolia.Application.Models.IndexSettings;

namespace Algolia.Application.Models.UpdateIndex
{
    public class UpdateIndexSettingsRequestDto
    {
        public string IndexName { get; set; }
        public IndexOptions IndexOptions { get; set; }

    }
}