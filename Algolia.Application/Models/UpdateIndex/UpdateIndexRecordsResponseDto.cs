using Algolia.Application.infrastructure;

namespace Algolia.Application.Models.UpdateIndex
{
    public class UpdateIndexRecordsResponseDto:ResponseModelBase
    {
        public UpdateIndexRecordsResponseDto(string message, bool success) : base(message, success)
        {
        }
    }
}