using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Interface
{
    public interface IHealth
    {
        int Health { get; set; }
        bool IsDead => Health <= 0;
    }
}
