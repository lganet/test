using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    [Route("v1/")]
    [ApiController]
    public class LgaController : ControllerBase
    {

        private readonly IServices _service;

        public LgaController(IServices services)
        {
            _service = services;
        }

        [HttpPost]
        [Route("item")]
        public ActionResult<Item> Post(Item item)
        {
            Item items = null;

            try
            {
                items = _service.AddItem(item);
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new { ErrorCode = 1, Message = ex.Message } );
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { ErrorCode = 500, Message = ex.Message });
            }          

            return items;
        }

        [HttpGet]
        [Route("item/{item?}")]
        public ActionResult<List<Item>> Get(String item)
        {
            List<Item> items = null;

            try
            {
                items = _service.GetItem(item);
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new { ErrorCode = 1, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { ErrorCode = 500, Message = ex.Message });
            }

            if (items is null)
            {
                return NotFound();
            }

            return items;
        }

    }
}