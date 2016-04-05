using IoTree.Server.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace IoTree.Server.Controllers
{
    public class PatternController : ApiController
    {
        private readonly IPatternManager patternManager;

        public PatternController(IPatternManager patternManager)
        {
            this.patternManager = patternManager;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json(patternManager.Current);
        }

        [HttpPost]
        public IHttpActionResult Set()
        {
            // Due to missing implementation in the currently stable version
            // of mono (4.2.1) we have to deserialize the body of a http
            // post -request manually. This will be fixed in mono 4.3.3.

            string httpBody;
            using (var reader = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                httpBody = reader.ReadToEnd();
            }

            if (String.IsNullOrWhiteSpace(httpBody))
                return BadRequest();

            var pattern = JsonConvert.DeserializeObject<List<PatternStep>>(httpBody);

            if (pattern != null && pattern.Any())
            {
                patternManager.Current = pattern;
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
