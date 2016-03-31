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
            ISoftPwmPin pin;
            if (!gpio.SoftPwmPins.TryGetValue(PinId.FromBroadcom(id), out pin))
            {
                return NotFound();
            }
            return Json(pin);
        }

        [HttpGet]
        public IHttpActionResult Set(int id, double value)
        {
            ISoftPwmPin pin;
            if (!gpio.SoftPwmPins.TryGetValue(PinId.FromBroadcom(id), out pin))
            {
                return NotFound();
            }
            pin.Value = value;
            return Ok();
        }
    }
}
