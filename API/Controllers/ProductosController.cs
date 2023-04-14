using Core.Entities;
using Core.Interfaces;
using Infraestructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ProductosController : BaseApiController
    {

        private readonly IUnitOfWork _unitOfWork;

        public ProductosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Producto>>> Get()
        {
            var productos = await _unitOfWork.Productos
                            .GetAllAsync();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var producto = await _unitOfWork.Productos
                            .GetByIdAsync(id);
            return Ok(producto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Producto>> Post(Producto producto)
        {
            _unitOfWork.Productos.Add(producto);
            _unitOfWork.Save();
            if (producto == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Post), new { id = producto.Id }, producto);
        }


    }
}
