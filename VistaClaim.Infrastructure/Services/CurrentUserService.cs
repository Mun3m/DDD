using System;
using System.Collections.Generic;
using System.Text;
using VistaClaim.Application.Common.Interfaces;

namespace VistaClaim.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string UserId => "1";
    }
}
