using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcurementManagementSystemServices.DTOs;
using ProcurementManagementSystemServices.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProcurementManagmentSystemAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SitesController : ControllerBase
    {
        private readonly ProcurementManagmentContext context;
        private readonly IMapper mapper;

        public SitesController(ProcurementManagmentContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("GetSites")]
        public ActionResult GetSitesOf()
        {
            string loggedOnUser = User.Identity.Name;
            List<UserSite> sites = this.context.UserSites.Where(site => site.Username == loggedOnUser).ToList<UserSite>();

            List<UserSiteDTO> listDTO = new List<UserSiteDTO>();
            foreach (UserSite us in sites)
            {
                UserSiteDTO dto = this.mapper.Map<UserSiteDTO>(us);
                Site site = this.context.Sites.Find(us.Site);
                dto.Name = site.Name;

                listDTO.Add(dto);
            }

            return Ok(listDTO);
        }

        // GET: api/Sites/5
        [HttpGet("{id}")]
        public ActionResult GetSite(int id)
        {
            var site = context.Sites.Find(id);

            if (site == null)
            {
                return NotFound();
            }

            SiteDTO siteDTO = this.mapper.Map<SiteDTO>(site);

            var address = this.context.Addresses.Find(siteDTO.AddressId);
            AddressDTO addressDTO = this.mapper.Map<AddressDTO>(address);

            var country = this.context.Countries.Find(address.Country);
            CountryDTO countryDTO = this.mapper.Map<CountryDTO>(country);
            addressDTO.CountryName = countryDTO.Name;

            siteDTO.Address = addressDTO;

            var contact = this.context.Contacts.Find(id);
            ContactDTO contactDTO = this.mapper.Map<ContactDTO>(contact);
            siteDTO.Contact = contactDTO;

            return Ok(siteDTO);
        }
    }
}
