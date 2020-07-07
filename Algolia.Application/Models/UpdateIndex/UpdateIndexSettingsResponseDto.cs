using Algolia.Application.infrastructure;

namespace Algolia.Application.Models.UpdateIndex
{
    public class UpdateIndexSettingsResponseDto:ResponseModelBase
    {
        public UpdateIndexSettingsResponseDto(string message, bool success) : base(message, success)
        {
        }
    }
}