using System;

namespace Algolia.Application.infrastructure
{
    public abstract class AuditDtoBase
    {
        public DateTime CreationDate { get; set; }    
        public Guid CreatorUserId { get; set; }    
    }
}