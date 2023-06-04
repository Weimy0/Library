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
    public class PeopleService
    {
        private ApplicationDbContext _db = null!;
        public PeopleService(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<People> GetPeoples()
            => _db.Peoples
            .Include(x => x.Post)
            .Include(x=>x.PeopleBooks)
                .ThenInclude(x=>x.Book)
            .Include(x=> x.User)
            .ToList();

        public People? GetPeople(int id)
            => GetPeoples()
            .SingleOrDefault(x => x.Id == id);

        public void Insert(People people)
        {
            _db.Peoples.Add(people);
            _db.SaveChanges();
        }

        public void Update(People people)
        {
            _db.Peoples.Update(people);
            _db.SaveChanges();
        }

        public void Delete(People people)
        {
            _db.Peoples.Remove(people);
            _db.SaveChanges();
        }
    }
}
