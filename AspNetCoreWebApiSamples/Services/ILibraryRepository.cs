using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreWebApiSamples.Entities;
using AspNetCoreWebApiSamples.Helpers;
using AspNetCoreWebApiSamples.Models;

namespace AspNetCoreWebApiSamples.Services
{
    public interface ILibraryRepository
    {
        PagedList<Author> GetAuthors(AuthorsResourceParameters authorsResourceParameters);
        Author GetAuthor(Guid authorId);
        IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds);
        void AddAuthor(Author author);
        void DeleteAuthor(Author author);
        void UpdateAuthor(Author author);
        bool AuthorExists(Guid authorId);
        IEnumerable<Book> GetBooksForAuthor(Guid authorId);
        Book GetBookForAuthor(Guid authorId, Guid bookId);
        void AddBookForAuthor(Guid authorId, Book book);
        void UpdateBookForAuthor(Book book);
        void DeleteBook(Book book);
        Task ApplyPatchAsync<TEntity>(TEntity entityName, List<PatchDto> patchDtos) where TEntity : class;
        bool Save();
    }
}
