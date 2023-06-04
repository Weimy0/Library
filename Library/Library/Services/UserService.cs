using Library.Data;
using Library.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class UserService
    {
        private ApplicationDbContext _db = null!;
        public UserService(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<User> GetUsers()
            => _db.Users
            .Include(x => x.People)
                .ThenInclude(x => x.PeopleBooks)
                    .ThenInclude(x=>x.Book)
            .Include(x => x.People)
                .ThenInclude(x => x.Post)
            .ToList();

        public User? GetUser(int id)
            => GetUsers()
            .SingleOrDefault(x => x.Id == id);

        public User GetUser(string login, string password)
            => GetUsers()
            .Single(x => x.Login == login && x.Password == password);

        public void Insert(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public void Update(User user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
        }

        public void Delete(User user)
        {
            _db.Users.Remove(user);
            _db.SaveChanges();
        }
    }
}
