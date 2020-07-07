using System;
using System.Collections.Generic;
using Algolia.Application.infrastructure;
using Algolia.Application.Models.IndexSettings;

namespace Algolia.Application.Models.CreateIndex
{
    public class CreateIndexRequestDto<T>:AuditDtoBase
    {
        public string IndexName { get; set; }
        public List<T> Data { get; set; }
    }
}