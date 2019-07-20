using System;
using Spice.Data;

namespace Spice.Areas.Admin.Controllers
{
    internal class AppplicatonDbContext
    {
        public object MenuItem { get; internal set; }

        public static implicit operator AppplicatonDbContext(ApplicationDbContext v)
        {
            throw new NotImplementedException();
        }
    }
}