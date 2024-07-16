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
    public partial class frmUpdateAccount : Form
    {
        private string username, password, usertype, status, editBy;
        public frmUpdateAccount(string username, string password, string usertype, string status, string editBy)
        {
            InitializeComponent();
            this.username = username;
            this.password = password;
            this.usertype = usertype;
            this.editBy = editBy;
            this.status = status;
        }
        Class1 updateAccount = new Class1("DESKTOP-108SV7E\\SQLEXPRESS", "CS311-2022", "itc311", "admin");
        
        private void validatePassword()
        {
            if (TxtPass.Text == "")
            {
                errorProvider1.SetError(TxtPass, "Password is Empty");
            }
            else if (TxtPass.TextLength < 6)
            {
                errorProvider1.SetError(TxtPass, "Password should be atleast 6 characters");
            }
            else
            {
                errorProvider1.SetError(TxtPass, "");
            }
        }

        private void validateUserType()
        {
            if (cmbUserType.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbUserType, "Select user type");
            }
            else
            {
                errorProvider1.SetError(cmbUserType, "");
            }
        }

        private int errorCount;

        public void countErrors()
        {
            errorCount = 0;
            foreach (Control c in errorProvider1.ContainerControl.Controls)
            {
                if (errorProvider1.GetError(c) != "")
                {
                    errorCount++;
                }
            }
        }

        private void frmUpdateAccount_Load(object sender, EventArgs e)
        {
            TxtUser1.Text = username;
            TxtPass.Text = password;
            if (usertype == "ADMINISTRATOR")
            {
                cmbUserType.SelectedIndex = 0;
            }
            else if (usertype == "TECHNICAL")
            {
                cmbUserType.SelectedIndex = 1;
            }
            else
            {
                cmbUserType.SelectedIndex = 2;
            }

            if (status == "ACTIVE")
            {
                rbActive.Checked = true;
            }
            else
            {
                rbInActive.Checked = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            validatePassword();
            validateUserType();

            countErrors();
            if (errorCount == 0)
            {
                string newStatus;
                if (rbActive.Checked)
                {
                    newStatus = rbActive.Text.ToUpper();
                }
                else
                {
                    newStatus = rbInActive.Text.ToUpper();
                }
                try
                {
                    updateAccount.executeSQL("UPDATE tblAccounts SET passWord = '" + TxtPass.Text + "' , userType = '" + cmbUserType.Text.ToUpper() +
                    "' , status = '" + newStatus + "' , lastUpdatedBy = '" + editBy + "' , dateUpdated = '" + DateTime.Now.ToString("dd-MM-yyyy") +
                    "' WHERE userName = '" + TxtUser1.Text + "'");
                    if (updateAccount.rowAffected > 0)
                    {
                        MessageBox.Show("Account Updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmAccounts updateFrm = (frmAccounts)Application.OpenForms["frmAccounts"];
                        updateFrm.dgvLoad();
                        this.Close();
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error on Add new Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtUser1_Click(object sender, EventArgs e)
        {
            TxtUser1.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = SystemColors.Control;
            cmbUserType.BackColor = SystemColors.Control;
        }

        private void cmbUserType_Click(object sender, EventArgs e)
        {
            panel3.BackColor = SystemColors.Control;
            TxtUser1.BackColor = SystemColors.Control;
            panel4.BackColor = Color.White;
            cmbUserType.BackColor = Color.White;
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            TxtPass.UseSystemPasswordChar = true;
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            TxtPass.UseSystemPasswordChar = false;
        }


    }
}
