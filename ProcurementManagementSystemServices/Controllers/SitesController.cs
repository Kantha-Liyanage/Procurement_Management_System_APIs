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
                listDTO.Add(dto);
            }

            return Ok(listDTO);
        }

        [HttpGet("GetSiteBudget")]
        public ActionResult GetSiteBudgetOf()
        {
            string loggedOnUser = User.Identity.Name;
            List<SiteBudget> budgets = this.context.SiteBudgets.Where(site => site.Supervisor == loggedOnUser).ToList<SiteBudget>();
            List<SiteBudgetDTO> listDTO = new List<SiteBudgetDTO>();
            foreach (SiteBudget sb in budgets)
            {
                SiteBudgetDTO dto = this.mapper.Map<SiteBudgetDTO>(sb);
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

            SiteDTO dto = this.mapper.Map<SiteDTO>(site);

            return Ok(dto);
        }
    }
}
