using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EduHomeProject.Data;

namespace EduHomeProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =Constants.AdminRole )]
    public class BaseController : Controller
    {

    }
}
