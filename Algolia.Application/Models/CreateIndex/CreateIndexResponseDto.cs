using Algolia.Application.infrastructure;

namespace Algolia.Application.Models.CreateIndex
{
    public class CreateIndexResponseDto:ResponseModelBase
    {
        public CreateIndexResponseDto(string message, bool success) : base(message, success)
        {

        }
    }
}