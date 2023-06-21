using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesPlatform.Domain.Commons
{
    /// <summary>
    /// IRepository inteface.
    /// </summary>
    /// <typeparam name="T">T.</typeparam>
    public interface IRepository<T>
        where T : IAggregateRoot
    {
    }
}
