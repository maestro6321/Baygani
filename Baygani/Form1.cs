using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Baygani.General;
using System.Data.SqlClient;
using Baygani.Screens;

namespace Baygani
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
             dataGridView1.DataSource = GetAllData();
             dataGridView1.Columns[0].Width = 35;
             dataGridView1.Columns[1].Width = 80;
             dataGridView1.Columns[2].Width = 80;
             dataGridView1.Columns[4].Width = 80;
             dataGridView1.Columns[5].Width = 80;
             dataGridView1.Columns[6].Width = 80;
             dataGridView1.Columns[7].Width = 80;
             dataGridView1.Columns[8].Width = 80;
        }

        private DataTable GetAllData()
        {
            DataTable dtrecord = new DataTable();
            using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
            {
                using (SqlCommand cmd=new SqlCommand("ups_SelectAllLetterForDataGridView", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dtrecord.Load(sdr);
                }
            }
            return dtrecord;
        }

        private void لیستاداراتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edarat eds = new Edarat();
            eds.ShowDialog();
        }

        private void نامهجدیدToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            NewLetter nl = new NewLetter();
            nl.ShowDialog();
        }

        private void بروزرسانیToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetAllData();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            


        }

        private void تغییرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    NewLetter nl = new NewLetter();
                    nl.LetterID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    nl.IsUpdate = true;
                    nl.ShowDialog();
                }
                catch (Exception)
                {


                }
            }
            
        }

        private void لیستپیوستهاینامهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    Attachment at = new Attachment();
                    at.LetterID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    at.Shomare_Name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    at.ShowDialog();
                }
                catch (Exception)
                {


                }
            }

        }

        private void پوشهپیشفرضپیوستهاToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DefaultAttachFolder daf = new DefaultAttachFolder();
            daf.ShowDialog();
        }
    }
}
