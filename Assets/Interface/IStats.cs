using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Interface
{
    public interface IStats
    {
        double Damage { get; set; }
        double Rate { get; set; }
        float Speed { get; set; }
        int Count { get; set; }
    }
}
