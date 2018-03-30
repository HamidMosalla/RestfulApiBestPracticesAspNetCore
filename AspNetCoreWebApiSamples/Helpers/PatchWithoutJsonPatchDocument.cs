using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AspNetCoreWebApiSamples.Helpers
{
    public class Variance
    {
        public string PropertyName { get; set; }
        public object Left { get; set; }
        public object Right { get; set; }
    }

    public static class TypeExtensions
    {
        public static bool IsNullOrEmpty<T>(this T value)
        {
            if (typeof(T) == typeof(string)) return string.IsNullOrEmpty(value as string);

            return value == null || value.Equals(default(T));
        }

        public static dynamic Cast(dynamic obj, Type castTo)
        {
            return Convert.ChangeType(obj, castTo);
        }

        public static string[] GetFilledProperties<T>(this T value)
        {
            if (value.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(value), $"{nameof(value)} cannot be null.");

            bool PropertyHasValue(PropertyInfo prop)
            {
                if (prop.GetValue(value) is IList list && list.Count == 0) return false;

                return prop.GetValue(value) == null
                    ? false
                    : !IsNullOrEmpty(Cast(prop.GetValue(value), prop.GetValue(value).GetType()));
            }

            return value.GetType()
                        .GetProperties()
                        .Where(PropertyHasValue)
                        .Select(p => p.Name)
                        .ToArray();
        }

        public static List<Variance> DetailedCompare<T>(this T left, T right)
        {
            var variances = new List<Variance>();

            PropertyInfo[] leftProps = left.GetType().GetProperties();

            foreach (PropertyInfo prop in leftProps)
            {
                var v = new Variance();

                v.PropertyName = prop.Name;
                v.Left = prop.GetValue(left);
                v.Right = prop.GetValue(right);

                if (v.Left == null) continue;

                if (!Equals(v.Left, v.Right)) variances.Add(v);

            }

            return variances;
        }
    }

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
