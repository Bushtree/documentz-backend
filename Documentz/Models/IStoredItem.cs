using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Documentz.Models
{
    public interface IStoredItem
    {
        string Id { get; }
        string Name { get; }

        string Description { get; }

        IList<string> Tags { get; }
    }
}
