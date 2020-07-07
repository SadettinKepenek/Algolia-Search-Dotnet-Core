using System.Collections.Generic;
using Algolia.Application.infrastructure;

namespace Algolia.Application.Models.CreateIndex
{
    public class SearchIndexResponseDto<T>:ResponseModelBase
    {
        public List<T> Data { get; set; }
        public int MaxResultCount { get; set; }
        public int TotalCount { get; set; }
        public SearchIndexResponseDto(string message, bool success, List<T> data,int maxResultCount,int totalCount) : base(message, success)
        {
            Message = message;
            Success = success;
            this.Data = data;
            this.MaxResultCount = maxResultCount;
            this.TotalCount = totalCount;
        }
    }
}