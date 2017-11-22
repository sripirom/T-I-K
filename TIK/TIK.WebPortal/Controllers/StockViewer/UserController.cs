using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TIK.WebPortal.Models.StockViewerViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TIK.WebPortal.Controllers.StockViewer
{
    [Route("api/stockViewer/[controller]")]
    public class UserController : Controller
    {
        // GET: api/values
        [HttpGet("{username}/get")]
        public UserAccountViewModel Get(string username)
        {
            return new UserAccountViewModel() { FirstName = "Treader", LastName = "Rich" };
        }

    }
}
