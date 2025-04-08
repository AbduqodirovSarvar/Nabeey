using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Configurations;
using Nabeey.Domain.Entities.Books;
using Nabeey.Domain.Entities.UserBalls;
using Nabeey.Domain.Entities.UserBookStatus;
using Nabeey.Domain.Entities.Users;
using Nabeey.Service.DTOs.UserBalls;
using Nabeey.Service.DTOs.UserBookStatus;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Extensions;
using Nabeey.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeey.Service.Services
{
    public class UserBallService : IUserBallService
    {
        private readonly IMapper mapper;
        private readonly IRepository<UserBall> ballRepository;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Book> bookRepository;

        public UserBallService(
            IMapper mapper,
            IRepository<UserBall> ballRepository,
            IRepository<User> userRepository,
            IRepository<Book> bookRepository)
        {
            this.mapper = mapper;
            this.bookRepository = bookRepository;
            this.userRepository = userRepository;
            this.ballRepository = ballRepository;
        }

        public async ValueTask<UserBallResultDto> AddAsync(UserBallCreationDto dto)
        {
            var existUser = await userRepository.SelectAsync(u => u.Id.Equals(dto.UserId))
                ?? throw new NotFoundException($"This user is not found with id : {dto.UserId}");

            var existBook = await bookRepository.SelectAsync(b => b.Id.Equals(dto.BookId))
                ?? throw new NotFoundException($"This book is not found with id : {dto.BookId}");

            var mapped = mapper.Map<UserBall>(dto);
            var result = await ballRepository.InsertAsync(mapped);
            await ballRepository.SaveAsync();
            return mapper.Map<UserBallResultDto>(result);
        }

        public async ValueTask<bool> DeleteAsync(long id)
        {
            var existStatus = await ballRepository.SelectAsync(s => s.Id.Equals(id))
                ?? throw new NotFoundException($"This status is not found with id : {id}");

            ballRepository.Delete(existStatus);
            await ballRepository.SaveAsync();
            return true;
        }

        public async ValueTask<UserBallResultDto> ModifyAsync(UserBallUpdateDto dto)
        {
            var existBall = await ballRepository.SelectAsync(s => s.Id.Equals(dto.Id))
                ?? throw new NotFoundException($"This status is not found with id : {dto.Id}");

            var existUser = await userRepository.SelectAsync(u => u.Id.Equals(dto.UserId))
                ?? throw new NotFoundException($"This user is not found with id : {dto.UserId}");

            var existBook = await bookRepository.SelectAsync(b => b.Id.Equals(dto.BookId))
                ?? throw new NotFoundException($"This book is not found with id : {dto.BookId}");

            existBall.BookId = dto.BookId ?? existBall.BookId;
            existBall.UserId = dto.UserId ?? existBall.UserId;
            existBall.Ball = dto.Ball ?? existBall.Ball;

            ballRepository.Update(existBall);
            await ballRepository.SaveAsync();
            return mapper.Map<UserBallResultDto>(existBall);
        }

        public async ValueTask<IEnumerable<UserBallResultDto>> RetrieveAllAsync(PaginationParams @params, Filter filter, long? userId = null, long? bookId = null)
        {
            var all = await this.ballRepository.SelectAll(
                        includes: new[] { "Book", "User" })
                        .ToPaginate(@params)
                        .ToListAsync();

            if (userId != null)
            {
                all = all.Where(x => x.UserId.Equals(userId)).ToList();
            }
            if (bookId != null)
            {
                all = all.Where(x => x.BookId.Equals(bookId)).ToList();
            }

            return mapper.Map<List<UserBallResultDto>>(all);
        }

        public async ValueTask<UserBallResultDto> RetrieveByIdAsync(long id)
        {
            var status = await ballRepository.SelectAsync(s => s.Id.Equals(id))
                ?? throw new NotFoundException($"This status is not found with id : {id}");

            return mapper.Map<UserBallResultDto>(status);
        }
    }
}
