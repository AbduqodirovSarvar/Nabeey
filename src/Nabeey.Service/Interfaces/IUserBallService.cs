using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.UserBalls;
using Nabeey.Service.DTOs.UserBookStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeey.Service.Interfaces
{
    public interface IUserBallService
    {
        ValueTask<UserBallResultDto> AddAsync(UserBallCreationDto dto);
        ValueTask<UserBallResultDto> ModifyAsync(UserBallUpdateDto dto);
        ValueTask<bool> DeleteAsync(long id);
        ValueTask<UserBallResultDto> RetrieveByIdAsync(long id);
        ValueTask<IEnumerable<UserBallResultDto>> RetrieveAllAsync(PaginationParams @params, Filter filter, long? userId = null, long? bookId = null);
    }
}
