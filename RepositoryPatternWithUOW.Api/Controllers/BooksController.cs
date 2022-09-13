using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public BooksController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("GetById")]
        public IActionResult GetById()
        {
            var include = new[] { "Author" };

            return Ok(unitOfWork.Books.GetById(1));
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var include = new[] { "Author" };

            return Ok(unitOfWork.Books.GetAll(include));
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName()
        {
            var include = new[] { "Author" };

            return Ok(unitOfWork.Books.Find(x => x.Title == "Test1", include));
        }

        [HttpGet("GetAllWithAuthorsName")]
        public IActionResult GetAllWithAuthorsName()
        {
            var include = new[] { "Author" };

            return Ok(unitOfWork.Books.FindAll(x => x.Title.Contains("Test"), include));
        }

        [HttpGet("GetOrder")]
        public IActionResult GetOrder()
        {
            var include = new[] { "Author" };

            return Ok(unitOfWork.Books.FindAll(x => x.Title.Contains("Test"), include, x=>x.Title, OrderBy.Descending));
        }

        [HttpGet("AddOne")]
        public IActionResult AddOne()
        {
            var item = new Book
            {
                Title = "Test7",
                AuthorId = 3
            };
            
            var result = unitOfWork.Books.Add(item);
            unitOfWork.Complete();

            var include = new[] { "Author" };
            var includeItem = unitOfWork.Books.Find(x => x.Title == item.Title, include);

            return Ok(includeItem);
        }

    }
}
