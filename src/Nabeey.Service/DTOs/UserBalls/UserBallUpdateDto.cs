using Nabeey.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeey.Service.DTOs.UserBalls
{
    public class UserBallUpdateDto
    {
        public long Id { get; set; }

        public long? UserId { get; set; }
        public long? BookId { get; set; }
        public double? Ball { get; set; }
    }
}
