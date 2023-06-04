using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class Post
    {
        public Post()
        {
            Peoples = new HashSet<People>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public ICollection<People> Peoples { get; set; }
    }
}
