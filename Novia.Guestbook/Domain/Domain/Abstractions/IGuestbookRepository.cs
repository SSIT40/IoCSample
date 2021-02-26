using Novia.Guestbook.Domain.Abstractions;
using Novia.Guestbook.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace Novia.Guestbook.Domain.Abstractions
{
    using Guestbook = Novia.Guestbook.Domain.Entities.Guestbook;
    public interface IGuestbookRepository : IRepository<Guestbook>
    {
        List<IGuestbookEntry> ListEntries(ISpecification<IGuestbookEntry> spec);
        IGuestbook Add(IGuestbook entity);
    }
}