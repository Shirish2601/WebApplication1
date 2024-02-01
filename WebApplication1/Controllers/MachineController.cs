using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IMachineRepository _machineRepository;

        public MachineController(IMachineRepository machineRepository)
        {
            _machineRepository = machineRepository;
        }

        [HttpGet("machinename")]
        public ActionResult GetAsset(string machinename)
        {
            try 
            {
                if (machinename != null)
                {
                    return Ok(_machineRepository.GetAsset(machinename));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
            return BadRequest("Please enter valid Machine name");
        }

        [HttpGet]
        public ActionResult GetAssets()
        {
            try
            {
                return Ok(_machineRepository.GetAssets());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
