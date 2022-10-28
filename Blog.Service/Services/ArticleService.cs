using AutoMapper;
using Blog.Domain.Entities;
using Blog.Service.DTOs;
using Blog.Service.Repositories;
using Blog.Service.Services.Interfaces;

namespace Blog.Service.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _repository;
        private readonly IMapper _mapper;

        public ArticleService(IArticleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ArticleResponse>> GetAllAsync()
        {
            IEnumerable<Article> list = await _repository.GetAllAsync();

            return list.Select(x => _mapper.Map<ArticleResponse>(x));
        }

        public async Task<ArticleResponse?> GetAsync(Guid id)
        {
            Article? entity = await _repository.GetAsync(id);

            if (entity is null)
            {
                return null;
            }

            return _mapper.Map<ArticleResponse>(entity);
        }

        public async Task<Guid> AddAsync(ArticleRequest request)
        {
            Article entity = _mapper.Map<Article>(request);

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, ArticleRequest request)
        {
            try
            {
                Article entity = _mapper.Map<Article>(request);
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
