using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.EntityFrameworkCore;

using Novia.Guestbook.Domain.Abstractions;
using Novia.Guestbook.Domain.Entities;

namespace Novia.Guestbook.Infrastructure.Data.Ef.Repositories
{
    using Guestbook = Domain.Entities.Guestbook;
    public class GuestbookRepository :
    EfRepository<GuestbookDbContext, Guestbook>,
    IGuestbookRepository
    {
        public GuestbookRepository(GuestbookDbContext dbContext) : base(dbContext)
        {
        }
        public IGuestbook Add(IGuestbook entity)
        {
            return base.Add(entity as Guestbook);
        }
        public override Guestbook GetById(int id)
        {
            return mDbContext.Guestbooks
            .Include(g => g.Entries)
            .FirstOrDefault(g => g.Id == id) as Guestbook;
        }
        public List<IGuestbookEntry> ListEntries(ISpecification<IGuestbookEntry> spec)
        {
            return mDbContext.GuestbookEntries
            .Where(spec.Criteria)
            .ToList();
        }
    }
}