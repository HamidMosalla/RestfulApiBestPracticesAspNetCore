using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AspNetCoreWebApiSamples.Entities;
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
        private readonly LibraryContext _context;


        public PatchWithoutJsonPatchDocument(
            IActionContextAccessor contextAccessor,
            IUrlHelper urlHelper,
            IMapper mapper,
            LibraryContext context)
        {
            _contextAccessor = contextAccessor;
            _urlHelper = urlHelper;
            _mapper = mapper;
            _context = context;
        }

        public async Task ApplyPatchBasedOnDto<TEntity, TDto, TKey>(TKey id, TDto dto) where TEntity : class
        {
            if (dto == null)
                throw new ArgumentNullException($"{nameof(dto)}", $"{nameof(dto)} cannot be null.");

            if (id.IsNullOrEmpty())
                throw new ArgumentNullException($"{nameof(id)}", $"{nameof(id)} cannot be null or empty.");

            var properties = dto.GetFilledProperties();

            var updateExpressions = properties.Select(BuildExpressionByPropertyName<TEntity>).ToArray();

            //dto.Id = id;

            var entityToUpdate = _mapper.Map<TEntity>(dto);
            _context.Attach(entityToUpdate);

            foreach (var property in properties)
            {
                _context.Entry(entityToUpdate).Property(property).IsModified = true;
            }

            await _context.SaveChangesAsync();
        }

        public Expression<Func<TEntity, object>> BuildExpressionByPropertyName<TEntity>(string property)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var member = Expression.Property(parameter, property);
            var conversion = Expression.Convert(member, typeof(object));
            return Expression.Lambda<Func<TEntity, Object>>(conversion, parameter);
        }
    }
}
