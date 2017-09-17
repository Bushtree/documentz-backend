using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Documentz.Models
{
    public interface IUser
    {
        string Id { get; }
        string Name { get; }
        string Email { get; }
        DateTime CreatedTime { get; }
    }
}
