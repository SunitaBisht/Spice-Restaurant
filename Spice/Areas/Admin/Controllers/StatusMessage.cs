using System;

namespace Spice.Areas.Admin.Controllers
{
    internal class StatusMessage
    {
        public StatusMessage()
        {
        }

        public static implicit operator string(StatusMessage v)
        {
            throw new NotImplementedException();
        }
    }
}