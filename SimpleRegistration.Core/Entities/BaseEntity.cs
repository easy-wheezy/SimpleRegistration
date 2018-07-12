using System;

namespace SimpleRegistration.Core.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset LatestUpdatedDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
