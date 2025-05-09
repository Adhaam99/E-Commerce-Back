using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public class AddressNotFoundException(string userName) : NotFoundException($"Address Not Found For User {userName}")
    {
    }
}
