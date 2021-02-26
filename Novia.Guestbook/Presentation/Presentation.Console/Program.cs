using Novia.Guestbook.Domain.Abstractions;
using Novia.Guestbook.Domain.Entities;

using System;
using Microsoft.Extensions.DependencyInjection;
using Novia.Guestbook.Infrastructure.Data.Ef;

namespace Novia.Guestbook.Presentation.Console
{
    using Console = System.Console;
    using Guestbook = Novia.Guestbook.Domain.Entities.Guestbook;
    using GuestbookEntry = Novia.Guestbook.Domain.Entities.GuestbookEntry;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            /////////////////////////////////////////////////////////////////////////////////////////////////
            //setup our services,
            var serviceCollection = new ServiceCollection();
            var bootStrapper = new Startup();
            bootStrapper.ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            using (EfGuestbookDbContext theContext = serviceProvider.GetService<EfGuestbookDbContext>())
            {
                ////////////////////////
                // hard work
                // The transient objects
                IGuestbook guestbook = serviceProvider.GetService<IGuestbook>();
                guestbook.Name = "Novia";
                IGuestbookEntry guestbookEntry = null;
                //#1
                guestbookEntry = serviceProvider.GetService<IGuestbookEntry>();
                guestbook.AddEntry(guestbookEntry);
                //#2
                guestbookEntry = serviceProvider.GetService<IGuestbookEntry>();
                guestbookEntry.Message = "Testing entry.";
                guestbook.AddEntry(guestbookEntry);
                // Add the transient object to a repository, which knows how to store
                IGuestbookRepository theBookRepository = serviceProvider.GetService<IGuestbookRepository>();
                theBookRepository.Add(guestbook);
                //-------
                IGuestbook theBook = theBookRepository.GetById(3);
                //IGuestbook theBook = theBookRepository.ListAll()
                // .ToList() // tuns the sql
                // .Where(theIteratorBook => theIteratorBook.Name == "Novia").FirstOrDefault();
                theBook.Name = "Novia2";
                theBookRepository.Update(theBook as Guestbook);
                //-------
                ////////////////////////
                // Commit to the database
                theContext.SaveChanges();
            }
        }
    }

}
