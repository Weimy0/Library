using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class PeopleBook
    {
        public int Id { get; set; }
        public int PeopleId { get; set; }
        public int BookId { get; set; }

        public People People { get; set; } = null!;
        public Book Book { get; set; } = null!;
    }
}
