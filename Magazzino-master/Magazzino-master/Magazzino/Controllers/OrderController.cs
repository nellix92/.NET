using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Magazzino.Domain.Entities;
using Magazzino.Domain.Infrastructure.Data;
using AutoMapper;
using Magazzino.Domain.DTOs;

namespace Magazzino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Ordine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdine()
        {
            return await _context.Order.ToListAsync();
        }

        // GET: api/Ordine/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrdine(Guid id)
        {
            var ordine = await _context.Order.FindAsync(id);

            if (ordine == null)
            {
                return NotFound();
            }

            return ordine;
        }

        // PUT: api/Ordine/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdine(Guid id, Order ordine)
        {
            if (id != ordine.Id)
            {
                return BadRequest();
            }

            _context.Entry(ordine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Ordine
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateOrder(List<OrderItemDTO> orderItems)
        {
            try
            {
                // Verifica se tutte le quantità richieste sono disponibili
                foreach (var orderItem in orderItems)
                {
                    var product = await _context.Products.FindAsync(orderItem.ProductId);
                    if (product == null || product.Quantities < orderItem.Quantity)
                    {
                        return BadRequest("Quantità non disponibile per uno o più prodotti.");
                    }
                }

                // Se tutte le quantità sono disponibili, crea un nuovo ordine
                var newOrder = new Order
                {
                    // Imposta altre proprietà dell'ordine se necessario
                    OrderDate = DateTime.UtcNow // Imposta la data dell'ordine su DateTime.UtcNow
                };
                _context.Order.Add(newOrder);

                foreach (var orderItem in orderItems)
                {
                    var product = await _context.Products.FindAsync(orderItem.ProductId);
                    if (product != null)
                    {
                        var newOrderItem = new OrderItem
                        {
                            ProductId = orderItem.ProductId,
                            Quantity = orderItem.Quantity,
                            UnitPrice = product.Price,
                            Order = newOrder
                        };
                        _context.OrderItems.Add(newOrderItem);
                    }
                }

                // Sottrai le quantità richieste dai prodotti disponibili
                foreach (var orderItem in orderItems)
                {
                    var product = await _context.Products.FindAsync(orderItem.ProductId);
                    product.Quantities -= orderItem.Quantity; // Sottrai la quantità richiesta dal prodotto disponibile
                }

                await _context.SaveChangesAsync();

                return Ok("Ordine creato con successo.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Errore durante la creazione dell'ordine: {ex.Message}");
            }
        }
        //public async Task<ActionResult<Ordine>> PostOrdine(CreateOrdineDTO ordineDTO)
        //{
        //   var ordine = _mapper.Map<CreateOrdineDTO,Ordine>(ordineDTO);
        //   _context.Ordine.Add(ordine);
        //   await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetOrdine", new { id = ordine.Id }, ordine);
        //}

        //[HttpPost]
        //public async Task<ActionResult<Product>> PostOrdine(CreateOrdineDTO OrdineDTO)
        //{
        //    _context.Ordine.Add(Ordine.CreateOrdine(OrdineDTO.quantities, OrdineDTO.Product));
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}

        // DELETE: api/Ordine/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdine(Guid id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdineExists(Guid id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
