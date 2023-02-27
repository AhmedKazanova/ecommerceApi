using Contracts;
using Entities.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiExplorerSettings(GroupName = "v1")]
   // [Authorize(Roles = "Admin")]
    public class ShippingController : ControllerBase
    {
        private readonly IRepositoryManger _repository;
        public ShippingController(IRepositoryManger repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            var allShipping = await _repository.Shipping.GetAllShipping(trackChanges: false);
           
            if (allShipping == null)
            {
                    return NotFound();
            }else
            {
                return Ok(new { message = "success", result = allShipping });
            }

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> getById([FromRoute] int id)
        {
            var shippingIsExist = await _repository.Shipping.GetShippingById(id, trackChanges: false);

            if (shippingIsExist == null)
            {
                return NotFound();
            }
            else
            {
              return Ok(new { message = "success", result = shippingIsExist });
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> deleteShipping(int id)
        {
            var shippingIsExist = await _repository.Shipping.GetShippingById(id, trackChanges: false);

            if (shippingIsExist == null)
            {
                return BadRequest(new { message = "shipping doesn't exist in the database" });
            }
            else
            {
                _repository.Shipping.DeleteShipping(shippingIsExist);
                await _repository.Save();
                return Ok(new { message = "success", result = shippingIsExist.ShippingId });
            }
        }

        [HttpPost]
        public async Task<IActionResult> addShipping([FromBody] Shipping shipping)
        {
            if (shipping == null)
            {
                return BadRequest(new { message = "Send Object Is Null" });

            }
            else if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }else
            {
                _repository.Shipping.CreateShipping(shipping);
                await _repository.Save();
                return Ok(new { message = "success" , result = shipping });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> putShipping([FromRoute] int id, [FromBody] Shipping shipping)
        {

            var shippingIsExist = await _repository.Shipping.GetShippingById(id, trackChanges: false);

            if (shipping == null)
            {
                return BadRequest(new { message = "Send Object Is Null" });

            }else if (shippingIsExist == null)
            {
                return BadRequest(new { message = "shipping doesn't exist in the database" });

            }else if (id != shipping.ShippingId)
            {
                return BadRequest(new { message = "id Route != Object Id" });
            }else
            {
                _repository.Shipping.PutShipping(shipping, trackChanges: false);
                await _repository.Save();
                return Ok(new { message = "success", result = shipping });
            }

        }



    }
}
