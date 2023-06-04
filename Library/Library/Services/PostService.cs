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
    public class PostService
    {
        private ApplicationDbContext _db = null!;
        public PostService(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Post> GetPosts()
            => _db.Posts
            .Include(x => x.Peoples)
                .ThenInclude(x=>x.User)
            .Include(x => x.Peoples)
            .ToList();

        public Post? GetPost(int id)
            => GetPosts()
            .SingleOrDefault(x => x.Id == id);

        public void Insert(Post post)
        {
            _db.Posts.Add(post);
            _db.SaveChanges();
        }

        public void Update(Post post)
        {
            _db.Posts.Update(post);
            _db.SaveChanges();
        }

        public void Delete(Post post)
        {
            _db.Posts.Remove(post);
            _db.SaveChanges();
        }
    }
}
