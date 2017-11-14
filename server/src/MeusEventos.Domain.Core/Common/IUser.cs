using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace MeusEventos.Domain.Core.Common
{
    public interface IUser
    {
        string Name { get; }
        Guid GetUserId();
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
