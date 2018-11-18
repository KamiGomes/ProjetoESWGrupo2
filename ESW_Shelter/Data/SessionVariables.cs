using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESW_Shelter.Data
{
    public class SessionVariables
    {
        public ISession _session;

        public SessionVariables(IHttpContextAccessor httpContextAccessor) //constructor
        {
            this._session = httpContextAccessor.HttpContext.Session;
        }
    }
}
