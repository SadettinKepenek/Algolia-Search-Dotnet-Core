using System.Threading.Tasks;
using Algolia.Application.Models.CreateIndex;
using Algolia.Application.Models.UpdateIndex;
using Algolia.Search.Models.Search;

namespace Algolia.Application.Domains.CreateIndex
{
    public interface IIndexService
    {
        Task<CreateIndexResponseDto> CreateIndexAsync<T>(CreateIndexRequestDto<T> dto) where T : class, new();
        Task<SearchIndexResponseDto<T>> SearchIndexAsync<T>(SearchIndexRequestDto dto) where T : class, new();
        Task<UpdateIndexSettingsResponseDto> UpdateIndexSettings<T>(UpdateIndexSettingsRequestDto dto) where T : class, new();
        Task<UpdateIndexRecordsResponseDto> UpdateRecordsAsync<T>(UpdateIndexRecordsRequestDto<T> dto) where T : class, new();
    }
}