using AutoMapper;
using ClienteAPI.Data;
using ClienteAPI.Dtos;
using ClienteAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClienteAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private ClienteContext _context;
        private IMapper _mapper;

        public ClienteController(ClienteContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    
        [HttpPost]
        public IActionResult CriarCliente([FromBody] CreateClienteDto clienteDto)
        {
            Cliente cliente = _mapper.Map<Cliente>(clienteDto);

            _context.Clientes.Add(cliente);
            _context.SaveChanges();
            return CreatedAtAction(nameof(BuscarClientePorId), new { Id = cliente.Id }, cliente);
        }

        [HttpGet]
        public IEnumerable<Cliente> BuscarClientes()
        {
            return _context.Clientes;
        }

        [HttpGet("{id}")]
        public IActionResult BuscarClientePorId(int id)
        {
            Cliente cliente = _context.Clientes.FirstOrDefault(cliente => cliente.Id == id);
            if(cliente != null)
            {
                return Ok(cliente);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult RefreshCliente(int id, [FromBody] UpdateClienteDto clienteDto)
        {
            Cliente cliente = _context.Clientes.FirstOrDefault(cliente => cliente.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            _mapper.Map(clienteDto, cliente);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaCliente(int id)
        {
            Cliente cliente = _context.Clientes.FirstOrDefault(cliente => cliente.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            _context.Remove(cliente);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
