using Novia.Guestbook.Domain.Abstractions;

using System;
using System.Collections.Generic;
using System.Text;

namespace Novia.Guestbook.Domain.Entities
{
    public class GuestbookEntry : Entity, IGuestbookEntry
    {
        public string EmailAddress { get; set; }
        public string Message { get; set; }
        public DateTimeOffset DateTimeCreated { get; set; } = DateTime.UtcNow;
        public IGuestbook BelongsTo { get; set; }
    }
}