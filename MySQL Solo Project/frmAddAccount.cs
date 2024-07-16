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
    public partial class frmAddAccount : Form
    {
        private string createdBy;
        public frmAddAccount(string createdBy)
        {
            InitializeComponent();
            this.createdBy = createdBy;
            
        }
        Class1 addNewAccount = new Class1("DESKTOP-108SV7E\\SQLEXPRESS", "CS311-2022", "itc311", "admin");
        private void validateUserName()
        {
            DataTable dt = addNewAccount.GetData("SELECT * FROM tblAccounts WHERE userName = '" + TxtUser1.Text + "'");
            if(TxtUser1.Text == "")
            {
                errorProvider1.SetError(TxtUser1, "Username is Empty");
            }
            else if(TxtUser1.TextLength < 4)
            {
                errorProvider1.SetError(TxtUser1, "Username should be atleast 6 characters");
            }
            else if(dt.Rows.Count > 0)
            {
                errorProvider1.SetError(TxtUser1, "Username is already exist!");
            }
            else
            {
                errorProvider1.SetError(TxtUser1, "");
            }
        }
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
            if(cmbUserType.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbUserType, "Select user type");
            }
            else
            {
                errorProvider1.SetError(cmbUserType, "");
            }
        }
        private int errorCount;
        public void ErrorCounts()
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

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            validateUserName();
            validateUserType();
            validatePassword();
            ErrorCounts();
            if (errorCount == 0)
            {
                if (errorCount == 0)
                {
                    try
                    {
                        DialogResult answer = MessageBox.Show("Are you sure you want to save this data?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (answer == DialogResult.Yes)
                        {
                            addNewAccount.executeSQL("INSERT INTO tblAccounts (userName, passWord, userType, status, createdBy, dateCreated) VALUES ('" + TxtUser1.Text +
                            "','" + TxtPass.Text + "','" + cmbUserType.Text.ToUpper() + "','ACTIVE','" + createdBy + "','" + DateTime.Now.ToString("MM/dd/yyyy ") + "')");
                            if (addNewAccount.rowAffected > 0)
                            {
                                MessageBox.Show("New Account Added!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                frmAccounts newFrm = (frmAccounts)Application.OpenForms["frmAccounts"];
                                newFrm.dgvLoad();
                                this.Close();
                            }
                        }
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(error.Message, "Error on Add new Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            TxtUser1.Clear();
            TxtPass.Clear();
        }

        private void BtnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_MouseDown_1(object sender, MouseEventArgs e)
        {
            TxtPass.UseSystemPasswordChar = false;
        }

        private void pictureBox3_MouseUp_1(object sender, MouseEventArgs e)
        {
            TxtPass.UseSystemPasswordChar = true;
        }

        private void TxtUser1_Click(object sender, EventArgs e)
        {
            TxtUser1.BackColor = Color.White;
            TxtPass.BackColor = SystemColors.Control;
        }

        private void TxtPass_Click(object sender, EventArgs e)
        {
            TxtUser1.BackColor = SystemColors.Control;
            TxtPass.BackColor = Color.White;
        }

        private void cmbUserType_Click(object sender, EventArgs e)
        {
            TxtUser1.BackColor = SystemColors.Control;
            TxtPass.BackColor = SystemColors.Control;
        }

    }
}
