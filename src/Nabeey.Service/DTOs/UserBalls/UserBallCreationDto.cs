using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeey.Service.DTOs.UserBalls
{
    public class UserBallCreationDto
    {
        public long UserId { get; set; }
        public long BookId { get; set; }
        public double Ball { get; set; }
    }
}
