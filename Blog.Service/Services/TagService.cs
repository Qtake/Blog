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

        public async Task<IEnumerable<TagResponse>> GetAllAsync()
        {
            IEnumerable<Tag> list = await _repository.GetAllAsync();

            return list.Select(x => _mapper.Map<TagResponse>(x));
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
            try
            {
                Tag entity = _mapper.Map<Tag>(request);
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
                Tag entity = _mapper.Map<Tag>(id);
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
