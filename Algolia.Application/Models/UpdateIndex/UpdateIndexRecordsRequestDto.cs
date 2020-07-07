using System.Collections.Generic;

namespace Algolia.Application.Models.UpdateIndex
{
    public class UpdateIndexRecordsRequestDto<T>
    {
        public string IndexName { get; set; }
        public List<T> Data { get; set; }
    }
}