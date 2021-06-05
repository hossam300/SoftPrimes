using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using SoftPrimes.Shared;
using SoftPrimes.Shared.ViewModels;

namespace IHelperServices
{
    public interface ISessionServices : _IHelperService
    {
        HttpContext HttpContext { get; set; }
        string UserId { get; }
        string UserName { get; }
        int? RoleId { get; }
        int UserRoleId { get; set; }
        int? OrganizationId { get; }
        string UserTokenId { get; set; }
        //string MachineName { get; }
        //string MachineIP { get; }
        //string Browser { get; }
        //string Url { get; }
        string Culture { get; set; }
        bool CultureIsArabic { get; }

        string ApplicationType { get; set; }
        string EmployeeFullNameAr { get; }
        string EmployeeFullNameEn { get; }
        string OrganizationNameAr { get; }
        string OrganizationNameEn { get; }
        string RoleNameAr { get; }
        string RoleNameEn { get; }
        string ClientIP { get; }
        string FaxUserId { get; }

        void SetAuthTicket(string username, AuthTicketDTO authTicket);
        AuthTicketDTO GetAuthTicket(string username);
        void ClearSessionsExcept(params string[] keys);
    }
}
