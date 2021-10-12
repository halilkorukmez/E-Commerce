using Clebra.Loopus.Model;
using Clebra.Loopus.Service.AboutDataService;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clebra.Loopus.API.Controllers
{
        [ApiController, Route("[controller]")]
        public class AboutController : ControllerBase
        {
            private readonly IAboutService aboutService;
            readonly Logger logger = LogManager.GetCurrentClassLogger();
            public AboutController(IAboutService aboutService)
            {
                this.aboutService = aboutService;
            }

            [HttpGet("[action]")]
            public async Task<IActionResult> GetAsync([FromHeader] Guid aboutId)
            {
                try
                {
                    logger.Info("İşlem Gerçekleşti");
                    var about = await aboutService.GetAsync(g => g.Id == aboutId);
                    if (about != null)
                        return Ok(about);
                    else
                        return NoContent();
                }
                catch (Exception e)
                {
                    logger.Error(e, "HATA");
                    return BadRequest(e);
                }

            }


            [HttpGet("[action]")]
            public async Task<IActionResult> GetList()
            {
                try
                {
                    logger.Info("İşlem Gerçekleşti");
                    var about = await aboutService.GetListAsync(g => g.IsActive);

                    if (about?.Count() > 0)
                        return Ok(about);
                    else
                        return NoContent();
                }
                catch (Exception e)
                {
                    logger.Error(e, "HATA");
                    return BadRequest(e);
                }
            }

            [HttpPost("[action]")]
            public async Task<IActionResult> SubmitChangeAsync([FromBody] About entity)
            {
                try
                {
                    logger.Info("İşlem Gerçekleşti");
                    await aboutService.SubmitChangeAsync(entity);
                    return Ok();
                }
                catch (Exception e)
                {
                    logger.Error(e, "HATA");
                    return BadRequest(e);
                }

            }
        }
    }

