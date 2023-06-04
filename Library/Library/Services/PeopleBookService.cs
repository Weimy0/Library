using Library.Data;
using Library.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class PeopleBookService
    {
        private ApplicationDbContext _db = null!;
        public PeopleBookService(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<PeopleBook> GetPeopleBooks()
            => _db.PeopleBooks
            .Include(x => x.People)
            .Include(x => x.Book)
            .ToList();

        public PeopleBook? GetPeopleBook(int id)
            => GetPeopleBooks()
            .SingleOrDefault(x => x.Id == id);

        public void Insert(PeopleBook peopleBook)
        {
            _db.PeopleBooks.Add(peopleBook);
            _db.SaveChanges();
        }

        public void Update(PeopleBook peopleBook)
        {
            _db.PeopleBooks.Update(peopleBook);
            _db.SaveChanges();
        }

        public void Delete(PeopleBook peopleBook)
        {
            _db.PeopleBooks.Remove(peopleBook);
            _db.SaveChanges();
        }

        public void Delete(List<PeopleBook> peopleBooks, BookService bookService)
        {
            foreach (var peopleBook in peopleBooks)
            {
                var book = bookService.GetBooks().SingleOrDefault(x => x.Id == peopleBook.BookId);
                if (book != null)
                {
                    book.Count += 1;
                    bookService.Update(book);
                }

                _db.PeopleBooks.Remove(peopleBook);
            }

            _db.SaveChanges();
        }
    }
}
