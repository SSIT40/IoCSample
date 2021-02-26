using Novia.Guestbook.Application.Abstractions;
using Novia.Guestbook.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Novia.Guestbook.Application.Services
{
    using Novia.Guestbook.Application.Abstractions.Dtos;
    using Novia.Guestbook.Domain.Entities;
    using Guestbook = Domain.Entities.Guestbook;
    public class GuestbookManagement : IGuestbookManagement
    {
        private IGuestbookRepository mGuestBookRepository;
        public GuestbookManagement(IGuestbookRepository guestbookRepository)
        {
            mGuestBookRepository = guestbookRepository;
        }
        public GuestbookDto Add(string sName)
        {
            Guestbook newBook = Guestbook.CreateGuestbook(sName);
            mGuestBookRepository.Add(newBook);
            // Mapping domain object to ui object
            GuestbookDto newBookDto = new GuestbookDto { Name = newBook.Name, Id = newBook.Id };
            return newBookDto;
        }
        public IEnumerable<GuestbookDto> ListAll()
        {
            var theGuestbooks = mGuestBookRepository.ListAll();
            // Mapping all domain objects to ui objects
            List<GuestbookDto> theGuestbookDtos = theGuestbooks.Select(entry =>
            new GuestbookDto { Name = entry.Name, Id = entry.Id }).ToList();
            return theGuestbookDtos;
        }
        public bool Remove(GuestbookDto theGuestbook)
        {
            Guestbook theBookToDelete = mGuestBookRepository.GetById(theGuestbook.Id);
            if (theBookToDelete != null)
            {
                mGuestBookRepository.Delete(theBookToDelete);
                return true;
            }
            return false;
        }
        public bool Modify(GuestbookDto theGuestbook)
        {
            Guestbook theBookToModify = mGuestBookRepository.GetById(theGuestbook.Id);
            if (theBookToModify != null)
            {
                // Mapping all ui objects to domain objects
                theBookToModify.Name = theGuestbook.Name;
                // fill in the guestbookentry list!
                mGuestBookRepository.Update(theBookToModify);
                return true;
            }
            return false;
        }
        public GuestbookDto FindById(int Id)
        {
            Guestbook theBook = mGuestBookRepository.GetById(Id);
            if (theBook != null)
            {
                // Mapping the domain objects to ui objects
                GuestbookDto theBookDto = new GuestbookDto
                {
                    Name = theBook.Name,
                    Id = theBook.Id
                };
                return theBookDto;
            }
            return null;
        }
    }
}