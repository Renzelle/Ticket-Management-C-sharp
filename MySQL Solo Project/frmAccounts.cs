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
    public partial class frmAccounts : Form
    {
        private string username;

        public frmAccounts(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        Class1 accounts = new Class1("DESKTOP-108SV7E\\SQLEXPRESS", "CS311-2022", "itc311", "itc311");
        private void frmAccounts_Load(object sender, EventArgs e)
        {
            dgvLoad();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = accounts.GetData("SELECT userName, passWord, userType, status, createdBy FROM tblAccounts WHERE username <> '" + username + 
                    "'AND(userName LIKE '%" + txtSearch.Text + "%' OR userType LIKE '%" + txtSearch.Text + "%') ORDER BY userName ");
                dataGridView1.DataSource = dt;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on log in button", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void dgvLoad()
        {
            try
            {
                DataTable dt = accounts.GetData("SELECT userName, passWord, userType, status, createdBy FROM tblAccounts WHERE username <> '" + username + "' ORDER BY userName ");
                dataGridView1.DataSource = dt;
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message, "Errorn on Add account", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string editUsername, editPassword, editUserType, editStatus;
            editUsername = dataGridView1.Rows[rowSelected].Cells[0].Value.ToString();
            editPassword = dataGridView1.Rows[rowSelected].Cells[1].Value.ToString();
            editUserType = dataGridView1.Rows[rowSelected].Cells[2].Value.ToString();
            editStatus = dataGridView1.Rows[rowSelected].Cells[3].Value.ToString();
            frmUpdateAccount updateAccount = new frmUpdateAccount(editUsername, editPassword, editUserType, editStatus, username);
            updateAccount.Show();
        }
        private int rowSelected;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            rowSelected = dataGridView1.CurrentCell.RowIndex;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }


        private void btnCreate_Click(object sender, EventArgs e)
        {
            frmAddAccount createAccount = new frmAddAccount(username);
            createAccount.Show();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.ColumnIndex == 1 && e.Value != null)
            {
                e.Value = new string('*', e.Value.ToString().Length);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult answer = MessageBox.Show("Are you sure you want to delete this data?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    accounts.executeSQL("DELETE FROM tblAccounts WHERE username ='" + dataGridView1.Rows[rowSelected].Cells[0].Value.ToString() + "' ");
                    if (accounts.rowAffected > 0)
                    {
                        MessageBox.Show("Account deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        accounts.executeSQL("INSERT INTO tblDeleteLogs VALUES('" + DateTime.Now.ToString("MM/dd/yyyy ")+ "', '" + DateTime.Now.ToLongTimeString() + "', 'Account Management', '"+ dataGridView1.Rows[rowSelected].Cells[0].Value.ToString() + "', '"+username+"')");
                        frmAccounts newFrm = (frmAccounts)Application.OpenForms["frmAccounts"];
                        newFrm.dgvLoad();
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            frmAccounts_Load(sender, e);
        }

        private void btnPassword_Click(object sender, EventArgs e)
        {
            string usertype = dataGridView1.Rows[rowSelected].Cells[2].Value.ToString();
            if (usertype != "ADMINISTRATOR")
            {
                string editUsername, editPassword;
                editUsername = dataGridView1.Rows[rowSelected].Cells[0].Value.ToString();
                editPassword = dataGridView1.Rows[rowSelected].Cells[1].Value.ToString();
                frmChangePassword updateAccount = new frmChangePassword(editUsername, editPassword);
                updateAccount.Show();
            }
            else
            {
                MessageBox.Show("Usertype must either be USER or TECHNICAL", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
