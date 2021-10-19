using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcurementManagementSystemServices.DTOs;
using ProcurementManagementSystemServices.Models;

namespace ProcurementManagmentSystemAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly ProcurementManagmentContext context;
        private readonly IMapper mapper;

        public ContactsController(ProcurementManagmentContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public ActionResult GetContact(int id)
        {
            var contact = this.context.Contacts.Find(id);

            if (contact == null)
            {
                return NotFound();
            }

            ContactDTO dto = this.mapper.Map<ContactDTO>(contact);

            return Ok(dto);
        }
    }
}
