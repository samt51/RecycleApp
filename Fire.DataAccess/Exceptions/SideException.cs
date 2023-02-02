using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire.DataAccess.Exceptions
{
    public class SideException:Exception
    {
        public SideException(string message):base(message)
        {

        }
    }
}
