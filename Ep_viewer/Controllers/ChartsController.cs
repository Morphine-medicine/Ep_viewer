using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ep_viewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly DBLab1Context _context;

        public ChartsController(DBLab1Context context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var Doctors = _context.Hospitals.Include(d => d.Doctors).ToList();

            List<object> hostitalDoctors = new List<object>();
            hostitalDoctors.Add(new[] { "Больница", "Количество врачей" });

            foreach (var c in Doctors)
            {
                hostitalDoctors.Add(new object[] { c.Location, c.Doctors.Count() });
            }
            return new JsonResult(hostitalDoctors);
        }

        [HttpGet("JsonData1")]
        public JsonResult JsonData1()
        {
            var Definitions = _context.EpisodeDefinitions.Include(d => d.Episodes).ToList();

            List<object> DefinitionsEpisodes = new List<object>();
            DefinitionsEpisodes.Add(new[] { "Больница", "Количество врачей" });

            foreach (var c in Definitions)
            {
                DefinitionsEpisodes.Add(new object[] { c.Definition, c.Episodes.Count() });
            }
            return new JsonResult(DefinitionsEpisodes);
        }
    }
}
