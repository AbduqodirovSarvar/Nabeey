using Nabeey.Domain.Entities.Books;
using Nabeey.Domain.Entities.Users;
using Nabeey.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeey.Service.DTOs.UserBookStatus
{
    public class UserBookStatusCreationDto
    {
        public long UserId { get; set; }
        public long BookId { get; set; }
        public ReadingStatus Status { get; set; }
    }
}
