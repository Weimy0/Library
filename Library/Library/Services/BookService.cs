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
    public class BookService
    {
        private ApplicationDbContext _db = null!;
        public BookService(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Book> GetBooks()
            => _db.Books
            .Include(x => x.PeopleBooks)
                .ThenInclude(x=>x.People)
            .ToList();

        public Book? GetBook(int id)
            => GetBooks()
            .SingleOrDefault(x => x.Id == id);

        public void Insert(Book book)
        {
            _db.Books.Add(book);
            _db.SaveChanges();
        }

        public void Update(Book book)
        {
            _db.Books.Update(book);
            _db.SaveChanges();
        }

        public void Delete(Book book)
        {
            _db.Books.Remove(book);
            _db.SaveChanges();
        }
    }
}
