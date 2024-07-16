using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySQL_Solo_Project
{
    public partial class frmDeleteAccountLogs : Form
    {
        private string username;
        public frmDeleteAccountLogs(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        Class1 deleteAccounts = new Class1("DESKTOP-108SV7E\\SQLEXPRESS", "CS311-2022", "itc311", "admin");
        private void frmLogs_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = deleteAccounts.GetData("SELECT date, time, module, ID, deletedFile FROM tblDeleteLogs WHERE date <> '" + username + "' ORDER BY date DESC ");
                dataGridView1.DataSource = dt;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on table account", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
