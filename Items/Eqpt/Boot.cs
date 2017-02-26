using Items.GNRT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items.Eqpt
{
    public class Boot:EquipmentSingle
    {
        public Boot(EnvironmentVariable ev):base(ev)
        {
            name = "boot";

        }
    }
}
