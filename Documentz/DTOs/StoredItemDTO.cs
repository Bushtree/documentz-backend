using Documentz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Documentz.DTOs
{
    public class StoredItemDTO : IStoredItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<string> Tags { get; set; }

        public DateTime CreationTime => throw new NotImplementedException();

        public DateTime LastModified => throw new NotImplementedException();
    }
}
