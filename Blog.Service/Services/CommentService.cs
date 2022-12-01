using AutoMapper;
using Blog.Domain.Entities;
using Blog.Service.DTOs;
using Blog.Service.Repositories;
using Blog.Service.Services.Interfaces;

namespace Blog.Service.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IQueryable<CommentResponse>> GetAllAsync()
        {
            IQueryable<Comment> list = await _repository.GetAllAsync();

            return list.Select(x => _mapper.Map<CommentResponse>(x));
        }

        public async Task<CommentResponse?> GetAsync(Guid id)
        {
            Comment? entity = await _repository.GetAsync(id);

            if (entity is null)
            {
                return null;
            }

            return _mapper.Map<CommentResponse>(entity);
        }

        public async Task<Guid> AddAsync(CommentRequest request)
        {
            Comment entity = _mapper.Map<Comment>(request);

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, CommentRequest request)
        {
            try
            {
                Comment entity = _mapper.Map<Comment>(request);
                await _repository.UpdateAsync(id, entity);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            try
            {
                await _repository.RemoveAsync(id);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
