using Items.GNRT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items.Eqpt
{
    class Helmet:EquipmentSingle
    {
        public Helmet(EnvironmentVariable ev):base(ev)
        {
            name = "Helmet";

        }
    }
}
