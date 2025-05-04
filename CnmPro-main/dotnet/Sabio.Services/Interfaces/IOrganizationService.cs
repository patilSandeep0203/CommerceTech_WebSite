using Sabio.Models.Domain;
using Sabio.Models.Domain.Organizations;
using Sabio.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data;

namespace Sabio.Models.Interfaces
{
    public interface IOrganizationService
    {
        int Create(OrganizationAddRequest model, int currentUserId);
        Organization GetOrganizationById(int id);
        Organization GetOrganizationByCreatedBy(int pageIndex, int pageSize, int userId);
        Paged<Organization> GetAll(int pageIndex, int pageSize);
        void Update(OrganizationUpdateRequest model, int userId);
        void DeleteOrganizationById(int id);
        List<LookUp> GetOrganizationTypes();
        List<State> GetStates();
        Organization MapSingleOrganization(IDataReader reader, ref int startingIndex);
    }
}