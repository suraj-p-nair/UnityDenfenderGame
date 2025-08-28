using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Interface
{
    public interface IStats
    {
        int Damage { get; set; }
        int Rate { get; set; }
        int Speed { get; set; }
    }
}
