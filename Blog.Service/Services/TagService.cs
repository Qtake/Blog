using AutoMapper;
using Blog.Domain.Entities;
using Blog.Service.DTOs;
using Blog.Service.Repositories;
using Blog.Service.Services.Interfaces;

namespace Blog.Service.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IQueryable<TagResponse>> GetAllAsync()
        {
            IQueryable<Tag> query = await _repository.GetAllAsync();

            return query.Select(x => _mapper.Map<TagResponse>(x));
        }

        public async Task<TagResponse?> GetAsync(Guid id)
        {
            Tag? entity = await _repository.GetAsync(id);

            if (entity is null)
            {
                return null;
            }

            return _mapper.Map<TagResponse>(entity);
        }

        public async Task<Guid> AddAsync(TagRequest request)
        {
            Tag entity = _mapper.Map<Tag>(request);

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, TagRequest request)
        {
            bool isExist = await _repository.Exist(x => x.ID == id);

            if (!isExist)
            {
                return false;
            }

            Tag entity = _mapper.Map<Tag>(request);
            await _repository.UpdateAsync(id, entity);

            return true;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            bool isExist = await _repository.Exist(x => x.ID == id);

            if (!isExist)
            {
                return false;
            }

            await _repository.RemoveAsync(id);

            return true;
        }

        public async Task<bool> ExistByName(string name)
        {
            bool isExist = await _repository.Exist(x => x.Name == name);

            return isExist;
        }

        public async Task<Tag?> GetByNameAsync(string name)
        {
            Tag? entity = await _repository.GetAsync(x => x.Name == name);

            if (entity is null)
            {
                return null;
            }

            return entity;
        }
    }
}
