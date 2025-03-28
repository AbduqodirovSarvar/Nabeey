using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Books;
using Nabeey.Domain.Entities.Users;
using Nabeey.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeey.Domain.Entities.UserBookStatus
{
    public class UserBookStatus : Auditable
    {
        public long UserId { get; set; }
        public User? User { get; set; }

        public long BookId { get; set; }
        public Book? Book { get; set; }

        public ReadingStatus Status { get; set; }
    }
}
