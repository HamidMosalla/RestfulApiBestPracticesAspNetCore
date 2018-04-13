using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AspNetCoreWebApiSamples.Helpers
{
    public class PatchWithoutJsonPatchDocument
    {
        private readonly IUrlHelper _urlHelper;
        private readonly IActionContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public PatchWithoutJsonPatchDocument(IActionContextAccessor contextAccessor, IUrlHelper urlHelper, IMapper mapper)
        {
            _contextAccessor = contextAccessor;
            _urlHelper = urlHelper;
            _mapper = mapper;
        }

        //public async Task ApplyPatch<TEntity, TDto, TKey>(TKey id, TDto dto)
        //{
        //    if (dto == null)
        //        throw new ArgumentNullException($"{nameof(dto)}", $"{nameof(dto)} cannot be null.");

        //    if (id.IsNullOrEmpty())
        //        throw new ArgumentNullException($"{nameof(id)}", $"{nameof(id)} cannot be null or empty.");

        //    var properties = dto.GetFilledProperties();

        //    var updateExpressions = properties.Select(BuildExpressionByPropertyName<TEntity>).ToArray();

        //    dto.Id = id;

        //    var entityToUpdate = _mapper.Map<TEntity>(dto);

        //    var rowVersion = await _repository.GetOneAsync<TEntity>(t => t.Id.ToString() == id.ToString(), null, true);

        //    entityToUpdate.Version = rowVersion.Version;
        //    entityToUpdate.ModifiedByApplicationUser = GetCurrentUserIdOrDefault();

        //    _repository.Update(entityToUpdate, null, updateExpressions);

        //    await _repository.SaveAsync();
        //}

        //public Guid? GetCurrentUserIdOrDefault()
        //{
        //    var userId = _contextAccessor.ActionContext.HttpContext.GetUserId();

        //    if (string.IsNullOrEmpty(userId)) return null;

        //    return new Guid(_contextAccessor.ActionContext.HttpContext.GetUserId());
        //}

        public Expression<Func<TEntity, object>> BuildExpressionByPropertyName<TEntity>(string property)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var member = Expression.Property(parameter, property);
            var conversion = Expression.Convert(member, typeof(object));
            return Expression.Lambda<Func<TEntity, Object>>(conversion, parameter);
        }
    }
}
