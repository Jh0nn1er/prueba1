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
        public ActionResult<IEnumerable<Forms>> GetContactForms()
        {
            return Ok(_dbContext.Forms.ToList());
        }
    }
}