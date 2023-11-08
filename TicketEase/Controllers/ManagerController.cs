using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketEase.Application.DTO;
using TicketEase.Application.Interfaces.Services;
using TicketEase.Domain.Entities;

namespace TicketEase.Controllers
{
    [Route("api/managers")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerServices _managerService;

        public ManagerController(IManagerServices managerService)
        {
            _managerService = managerService;
        }

        //[HttpGet("paged")]
        //public IActionResult GetAllManagersByPagination(int perPage = 10, int page = 1, string user = "")
        //{
        //    try
        //    {
        //        var response = _managerService.GetAllManagerByPagination(perPage, page, user);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(500, new { message = "Internal Server Error" });
        //    }
        //}

        [HttpPost("Add")]
        public IActionResult AddManagers(Manager managerDTO)
        {
            try
            {
                var response = _managerService.AddManager(managerDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        [HttpGet("GetById")]
        public IActionResult GetManagersById(string id)
        {
            try
            {
                var response = _managerService.GetManagerById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        [HttpGet("Edit")]

        public IActionResult EditManager(string id, EditManagerDto managerDTO)
        {
            try
            {
                var response = _managerService.EditManager(id, managerDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }


    }
}
