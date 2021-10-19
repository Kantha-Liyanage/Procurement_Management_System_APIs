using AutoMapper;
using ProcurementManagementSystemServices.DTOs;
using ProcurementManagementSystemServices.Models;

namespace ProcurementManagementSystemServices.Profiles
{
    public class AutoProfile : Profile
    {
        public AutoProfile() {
            CreateMap<Address, AddressDTO>();
            CreateMap<Contact, ContactDTO>();
            CreateMap<Country, CountryDTO>();
            CreateMap<Depot, DepotDTO>();
            CreateMap<MaterialCategory, MaterialCategoryDTO>();
            CreateMap<Material, MaterialDTO>();
            CreateMap<PurchaseRequisition, PurchaseRequisitionDTO>();
            CreateMap<PurchaseRequisitionItem, PurchaseRequisitionItemDTO>();
            CreateMap<Site, SiteDTO>();
            CreateMap<SiteBudget, SiteBudgetDTO>();
            CreateMap<Supplier, SupplierDTO>();
            CreateMap<UnitOfMeasure, UnitOfMeasureDTO>();
            CreateMap<UserSite, UserSiteDTO>();
        }
    }
}
