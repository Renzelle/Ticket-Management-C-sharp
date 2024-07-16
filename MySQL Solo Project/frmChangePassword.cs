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
    public partial class frmChangePassword : Form
    {
        private string username, password;
        public frmChangePassword(string username, string password)
        {
            InitializeComponent();
            this.username = username;
            this.password = password;
        }
        Class1 updatePassword = new Class1("DESKTOP-108SV7E\\SQLEXPRESS", "CS311-2022", "itc311", "admin");
        private void validatePassword()
        {
            if (txtNewPass.Text == "")
            {
                errorProvider1.SetError(txtNewPass, "Password is Empty");
            }
            else if (txtNewPass.TextLength < 6)
            {
                errorProvider1.SetError(txtNewPass, "Password should be atleast 6 characters");
            }
            else
            {
                errorProvider1.SetError(txtNewPass, "");
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
        private void changePasswordTech_Load(object sender, EventArgs e)
        {
            TxtPass.Text = password;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            validatePassword();

            countErrors();
            if (errorCount == 0)
            {
                try
                {
                    updatePassword.executeSQL("UPDATE tblAccounts SET passWord = '" + txtNewPass.Text + "'WHERE userName = '" + username + "'");
                    if (updatePassword.rowAffected > 0)
                    {
                        MessageBox.Show("Password Updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
