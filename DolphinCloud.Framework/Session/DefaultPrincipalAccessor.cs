using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DolphinCloud.Framework.Session
{
    public class DefaultPrincipalAccessor : IPrincipalAccessor
    {

        public virtual  ClaimsPrincipal Principal => Thread.CurrentPrincipal as ClaimsPrincipal;

        public static DefaultPrincipalAccessor Instance => new DefaultPrincipalAccessor();
    }
}
