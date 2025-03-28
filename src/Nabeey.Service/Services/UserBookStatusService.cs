using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Configurations;
using Nabeey.Domain.Entities.Books;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Domain.Entities.Quizzes;
using Nabeey.Domain.Entities.UserBookStatus;
using Nabeey.Domain.Entities.Users;
using Nabeey.Service.DTOs.UserBookStatus;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Extensions;
using Nabeey.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Nabeey.Service.Services
{
    public class UserBookStatusService : IUserBookStatusService
    {
        private readonly IMapper mapper;
        private readonly IRepository<UserBookStatus> statusRepository;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Book> bookRepository;

        public UserBookStatusService(
            IMapper mapper,
            IRepository<UserBookStatus> statusRepository,
            IRepository<User> userRepository,
            IRepository<Book> bookRepository)
        {
            this.mapper = mapper;
            this.bookRepository = bookRepository;
            this.userRepository = userRepository;
            this.statusRepository = statusRepository;
        }
        public async ValueTask<UserBookStatusResultDto> AddAsync(UserBookStatusCreationDto dto)
        {
            var existUser = await userRepository.SelectAsync(u => u.Id.Equals(dto.UserId))
                ?? throw new NotFoundException($"This user is not found with id : {dto.UserId}");

            var existBook = await bookRepository.SelectAsync(b => b.Id.Equals(dto.BookId))
                ?? throw new NotFoundException($"This book is not found with id : {dto.BookId}");

            var mapped = mapper.Map<UserBookStatus>(dto);
            var result = await statusRepository.InsertAsync(mapped);
            await statusRepository.SaveAsync();
            return mapper.Map<UserBookStatusResultDto>(result);
        }

        public async ValueTask<bool> DeleteAsync(long id)
        {
            var existStatus = await statusRepository.SelectAsync(s => s.Id.Equals(id))
                ?? throw new NotFoundException($"This status is not found with id : {id}");

            statusRepository.Delete(existStatus);
            await statusRepository.SaveAsync();
            return true;
        }

         public async ValueTask<UserBookStatusResultDto> ModifyAsync(UserBookStatusUpdateDto dto)
        {
            var existStatus = await statusRepository.SelectAsync(s => s.Id.Equals(dto.Id))
                ?? throw new NotFoundException($"This status is not found with id : {dto.Id}");

            var existUser = await userRepository.SelectAsync(u => u.Id.Equals(dto.UserId))
                ?? throw new NotFoundException($"This user is not found with id : {dto.UserId}");

            var existBook = await bookRepository.SelectAsync(b => b.Id.Equals(dto.BookId))
                ?? throw new NotFoundException($"This book is not found with id : {dto.BookId}");

            existStatus.BookId = dto.BookId;
            existStatus.UserId = dto.UserId;
            existStatus.Status = dto.Status;

            statusRepository.Update(existStatus);
            await statusRepository.SaveAsync();
            return mapper.Map<UserBookStatusResultDto>(existStatus);
        }

        public async ValueTask<IEnumerable<UserBookStatusResultDto>> RetrieveAllAsync(PaginationParams @params, Filter filter, long? userId = null, long? bookId = null)
        {
            var allStatus = await this.statusRepository.SelectAll(
                        includes: new[] { "Book", "User" })
                        .ToPaginate(@params)
                        .ToListAsync();

            if(userId != null)
            {
                allStatus = allStatus.Where(x => x.UserId.Equals(userId)).ToList();
            }
            if(bookId != null)
            {
                allStatus = allStatus.Where(x => x.BookId.Equals(bookId)).ToList();
            }

            return mapper.Map<List<UserBookStatusResultDto>>(allStatus);
        }

        public async ValueTask<UserBookStatusResultDto> RetrieveByIdAsync(long id)
        {
            var status = await statusRepository.SelectAsync(s => s.Id.Equals(id))
                ?? throw new NotFoundException($"This status is not found with id : {id}");

            return mapper.Map<UserBookStatusResultDto>(status);
        }
    }
}
