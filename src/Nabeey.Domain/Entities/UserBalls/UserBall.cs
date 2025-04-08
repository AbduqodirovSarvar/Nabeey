using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Books;
using Nabeey.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeey.Domain.Entities.UserBalls
{
    public class UserBall : Auditable
    {
        public long UserId { get; set; }
        public User? User { get; set; }

        public long BookId { get; set; }
        public Book? Book { get; set; }

        public double Ball { get; set; }
    }
}
