using IoTree.Gpio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IoTree.Server.Controllers
{
    public class ManualController : ApiController
    {
        private readonly IGpioManager gpio;

        public ManualController(IGpioManager gpio)
        {
            this.gpio = gpio;
        }
        
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Json(gpio.SoftPwmPins.Values
                .ToDictionary(p => p.Id.BroadcomId, p => p.Value));
        }
        
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            if (!PinId.IsValidBroadcomId(id))
                return NotFound();

            return Json(gpio.SoftPwmPins[PinId.FromBroadcom(id)]);
        }

        [HttpGet]
        public IHttpActionResult Set(int id, double value)
        {
            if (!PinId.IsValidBroadcomId(id))
                return NotFound();

            gpio.SoftPwmPins[PinId.FromBroadcom(id)].Value = value;
            return Ok();
        }
    }
}
