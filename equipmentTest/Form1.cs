using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using eT.Bll;
using Equipment;
using System.Reflection;

namespace equipmentTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

            //MessageBox.Show(dtManagers.Rows[0]["userid"].ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            EquipmentSingle equipment = new EquipmentSingle(Convert.ToInt32(txtLevel.Text),EquipmentSingle.EquipmentTypeEnum.Armor);
            Random rd = new Random();
            string str = "";
            str = equipment.name + "\n";
            for(int i=0;i<equipment.affixList.Count;i++){
                str +="+"+equipment.affixList[i].attribute.minValue + "-" + equipment.affixList[i].attribute.maxValue + " " + equipment.affixList[i].attribute.AttributeType + "\n";
            }

            lbEquipmentName.Text = str;
             */

            string str = "";
            var subTypeQuery = from t in Assembly.GetExecutingAssembly().GetTypes()
                               where IsSubClassOf(t, typeof(Equipment.Attribute))
                               select t;

            foreach (var type in subTypeQuery)
            {
                str += type+" ";
            }
            lbEquipmentName.Text = str;
        }


        static bool IsSubClassOf(Type type, Type baseType)
        {
            var b = type.BaseType;
            while (b != null)
            {
                if (b.Equals(baseType))
                {
                    return true;
                }
                b = b.BaseType;
            }
            return false;
        }
    }
}
