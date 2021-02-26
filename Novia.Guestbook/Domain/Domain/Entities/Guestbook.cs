using Novia.Guestbook.Domain.Abstractions;

using System;
using System.Collections.Generic;
using System.Text;


namespace Novia.Guestbook.Domain.Entities
{
    public class Guestbook : Entity, IGuestbook
    {

        public string Name { get; set; }

        public virtual IList<IGuestbookEntry> Entries { get; set; } = new List<IGuestbookEntry>();
        // the creation factory
        public void AddEntry(IGuestbookEntry entry)
        {
            Entries.Add(entry);
        }
        static public Guestbook CreateGuestbook(string sName)
        {
            Guestbook theNewQuestbook = new Guestbook { Name = sName };
            return theNewQuestbook;
        }
    }
}