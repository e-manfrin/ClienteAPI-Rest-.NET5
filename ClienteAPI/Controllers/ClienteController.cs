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
        //private static List<Cliente> clientes = new List<Cliente>();
        //private static int id = 1;

        [HttpPost]
        //public IActionResult CriarCliente([FromBody]Cliente cliente)
        public IActionResult CriarCliente([FromBody] CreateClienteDto clienteDto)
        {
            Cliente cliente = _mapper.Map<Cliente>(clienteDto);

            //Cliente cliente = new Cliente
            //{
                //Nome = clienteDto.Nome,
                //Endereco = clienteDto.Endereco,
                //Numero = clienteDto.Numero,
                //Cidade = clienteDto.Cidade,
                //Telefone = clienteDto.Telefone
            //};

            _context.Clientes.Add(cliente);
            _context.SaveChanges();
            return CreatedAtAction(nameof(BuscarClientePorId), new { Id = cliente.Id }, cliente);
            //cliente.Id = id++;
            //clientes.Add(cliente);
            //CreatedAtAction mostrar o status da requisição e onde o recurso foi criado
            //A lógica BuscarClientePorId é executada 
        }

        [HttpGet]
        //OK não é do tipo IEnumerable<Cliente> mas sim resultado de uma ação IActionResult
        //Retorna um 200 com lista vazia se não adicionar.
        public IEnumerable<Cliente> BuscarClientes()
        {
            return _context.Clientes;
        }

        [HttpGet("{id}")]
        //OK  e NotFound não são do tipo cliente mas sim resultado de uma ação IActionResult
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
        //public IActionResult RefreshCliente(int id, [FromBody] Cliente clienteNovo)
        public IActionResult RefreshCliente(int id, [FromBody] UpdateClienteDto clienteDto)
        {
            Cliente cliente = _context.Clientes.FirstOrDefault(cliente => cliente.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            //cliente.Nome = clienteDto.Nome;
            //cliente.Endereco = clienteDto.Endereco;
            //cliente.Numero = clienteDto.Numero;
            //cliente.Cidade = clienteDto.Cidade;
            //cliente.Telefone = clienteDto.Telefone;

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
