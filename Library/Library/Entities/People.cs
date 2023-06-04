using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class People
    {
        public People()
        {
            PeopleBooks = new HashSet<PeopleBook>();
        }

        public int Id { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public int PostId { get; set; }

        public Post Post { get; set; } = null!;
        public User User { get; set; } = null!;
        public ICollection<PeopleBook> PeopleBooks{ get; set; } = null!;

        [NotMapped]
        public string FullName => $"{Surname} {Name} {Lastname}";
        public string ShortFullName => $"{Surname} {Name[0]}. {Lastname[0]}.";
    }
}
