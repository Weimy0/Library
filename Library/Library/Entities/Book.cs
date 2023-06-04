using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class Book
    {
        public Book()
        {
            PeopleBooks = new HashSet<PeopleBook>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime DateOfPublication { get; set; }
        public string Author { get; set; } = null!;
        public int Count { get; set; }

        public ICollection<PeopleBook> PeopleBooks { get; set; }
    }
}
