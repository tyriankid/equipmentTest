using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Items.BaseClass
{
    public class Miscs:Item
    {

        public string name { get; set; }
        public override void Execute()
        {
            MessageBox.Show("Misc: " + name);
        }
    }
}
