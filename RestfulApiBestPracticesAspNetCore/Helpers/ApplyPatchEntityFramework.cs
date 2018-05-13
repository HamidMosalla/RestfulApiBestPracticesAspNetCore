using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspNetCoreWebApiSamples.Entities;
using AspNetCoreWebApiSamples.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApiSamples.Helpers
{
    public class EntityFrameworkPatch
    {
        //for when we can't use JsonPatchDocument, maybe because we don't use Json and use something like message pack
        private readonly LibraryContext _libraryContext;

        public EntityFrameworkPatch(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public async Task ApplyPatch<TEntity, TKey>(TEntity entityName, List<PatchDto> patchDtos) where TEntity : class
        {
            var nameValuePairProperties = patchDtos.ToDictionary(a => a.PropertyName, a => a.PropertyValue);

            var dbEntityEntry = _libraryContext.Entry(entityName);
            dbEntityEntry.CurrentValues.SetValues(nameValuePairProperties);
            dbEntityEntry.State = EntityState.Modified;
            await _libraryContext.SaveChangesAsync();

            /* example of what it accept over http, note that property names should match the entity
                 [
                    {
                     "PropertyName": "ModifiedDate",
                     "PropertyValue": null
                    },
                    {
                     "PropertyName": "CreatedDate",
                     "PropertyValue": "2020-01-30T12:53:51.6240518"
                    }
                ]
             */
        }
    }
}

/* https://codereview.stackexchange.com/questions/37304/update-only-modified-fields-in-entity-framework
 public virtual void Update(T entity)
    {
        //dbEntityEntry.State = EntityState.Modified;

        //Ensure only modified fields are updated.
        var dbEntityEntry = DbContext.Entry(entity);
        foreach (var property in dbEntityEntry.OriginalValues.PropertyNames)
        {
            var original = dbEntityEntry.OriginalValues.GetValue<object>(property);
            var current = dbEntityEntry.CurrentValues.GetValue<object>(property);
            if (original != null && !original.Equals(current))
                dbEntityEntry.Property(property).IsModified = true;
        }
    }
*/
