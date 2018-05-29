using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCoreWebApiSamples.Entities;
using AspNetCoreWebApiSamples.Helpers;
using AspNetCoreWebApiSamples.Models;
using AspNetCoreWebApiSamples.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace RestfulApiBestPracticesAspNetCore.Controllers
{
    [Route("api/authorcollections")]
    public class AuthorCollectionsController : Controller
    {
        private ILibraryRepository _libraryRepository;

        public AuthorCollectionsController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpPost]
        public IActionResult CreateAuthorCollection([FromBody] IEnumerable<AuthorForCreationDto> authorCollection)
        {
            if (authorCollection == null) return BadRequest();

            var authorEntities = Mapper.Map<IEnumerable<Author>>(authorCollection);

            foreach (var author in authorEntities)
            {
                _libraryRepository.AddAuthor(author);
            }

            if (!_libraryRepository.Save()) throw new Exception("Creating an author collection failed on save.");

            var authorCollectionToReturn = Mapper.Map<IEnumerable<AuthorDto>>(authorEntities);

            var idsAsString = string.Join(",", authorCollectionToReturn.Select(a => a.Id));

            return CreatedAtRoute("GetAuthorCollection", new { ids = idsAsString }, authorCollectionToReturn);
        }

        // It take in this form:
        //get-blog-stories-by-ids/73448281-b9bb-4f99-ecf1-08d5bf2e80d4, 25155221-c07b-4b30-ecf7-08d5bf2e80d4, 487de396-43b3-457e-ecfc-08d5bf2e80d4
        [HttpGet("({ids})", Name = "GetAuthorCollection")]
        public IActionResult GetAuthorCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var authorEntities = _libraryRepository.GetAuthors(ids);

            if (ids.Count() != authorEntities.Count())
            {
                return NotFound();
            }

            var authorsToReturn = Mapper.Map<IEnumerable<AuthorDto>>(authorEntities);
            return Ok(authorsToReturn);
        }
    }
}
