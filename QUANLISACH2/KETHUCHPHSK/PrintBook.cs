using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace KETHUCHPHSK
{
    public partial class PrintBook : Form
    {
        public PrintBook()
        {
            InitializeComponent();
        }
        public void cboMon()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qls"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Select sMaMon from Subject", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter adp = new SqlDataAdapter("Select sMaMon from Subject", cnn))
                    {
                        DataTable db = new DataTable();
                        adp.Fill(db);
                        cbomaMon.DataSource = db;
                        cbomaMon.DisplayMember = "sMaMon";
                    }
                }
            }
        }

        private void PrintBook_Load(object sender, EventArgs e)
        {
            cboMon();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qls"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_book";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@mamon", SqlDbType.Char).Value = cbomaMon.Text;
                    cnn.Open();
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        DataTable db = new DataTable();
                        adp.Fill(db);
                        DesignPrintBook rptB = new DesignPrintBook();
                        rptB.SetDataSource(db);
                        PrintBook pb = new PrintBook();
                        pb.crystalReportViewer1.ReportSource = rptB;
                        pb.Show();

                    }
                }
            }
        }
    }
}
