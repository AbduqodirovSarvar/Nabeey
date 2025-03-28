using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.Quizzes;
using Nabeey.Service.DTOs.UserBookStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeey.Service.Interfaces
{
    public interface IUserBookStatusService
    {
        ValueTask<UserBookStatusResultDto> AddAsync(UserBookStatusCreationDto dto);
        ValueTask<UserBookStatusResultDto> ModifyAsync(UserBookStatusUpdateDto dto);
        ValueTask<bool> DeleteAsync(long id);
        ValueTask<UserBookStatusResultDto> RetrieveByIdAsync(long id);
        ValueTask<IEnumerable<UserBookStatusResultDto>> RetrieveAllAsync(PaginationParams @params, Filter filter, long? userId = null, long? bookId = null);
    }
}
