using Baygani.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baygani.Screens
{
    public partial class Edarat : Form
    {
        public Edarat()
        {
            InitializeComponent();
        }

        private void Edarat_Load(object sender, EventArgs e)
        {
            EdaratlistBox.DataSource = GetAllData();
            EdaratlistBox.DisplayMember = "name_edare";
            EdaratlistBox.ValueMember = "ID";
        }

        private DataTable GetAllData()
        {
            DataTable dtrecord = new DataTable();
            using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
            {
                using(SqlCommand cmd=new SqlCommand("Select * from tblEdarat", con))
                {
                    con.Open();
                    SqlDataReader dtr = cmd.ExecuteReader();
                    dtrecord.Load(dtr);
                }
            }
            return dtrecord;
        }


    }
}
