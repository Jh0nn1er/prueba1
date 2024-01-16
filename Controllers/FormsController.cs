using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPIProduco.Model;
using WebAPIProduco.Data; 


namespace WebAPIProduco.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly DataContext _dbContext;
        public FormsController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Form>> GetContactForms()
        {
            return Ok(_dbContext.Forms.ToList());
        }
        [HttpPost]
        public async Task<IActionResult> CreateForms([FromBody] Form formData)
        {
            if (_dbContext.Forms.FirstOrDefault(v => v.Email.ToLower() == formData.Email.ToLower()) != null)
            {
                ModelState.AddModelError("mailExists", "a form with that email already exists");
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

             Form newForm = new()
                {
                    Id= formData.Id,
                    FullName = formData.FullName,
                    Email = formData.Email,
                    DocumentType = formData.DocumentType,
                    Identifier = formData.Identifier,
                    Comment = formData.Comment,
                };
                _dbContext.Add(newForm);
                await _dbContext.SaveChangesAsync();

                return Ok(new { id = newForm.Id });
           
        }
    }
}