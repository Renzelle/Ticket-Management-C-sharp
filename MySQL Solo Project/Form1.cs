using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MySQL_Solo_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TxtUser_Click(object sender, EventArgs e)
        {
            TxtUser.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = SystemColors.Control;
            TxtPass.BackColor = SystemColors.Control;
        }

        private void TxtPass_Click(object sender, EventArgs e)
        {
            TxtPass.BackColor = Color.White;
            panel4.BackColor = Color.White;
            TxtUser.BackColor = SystemColors.Control;
            panel3.BackColor = SystemColors.Control;
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            TxtPass.UseSystemPasswordChar = false;
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            TxtPass.UseSystemPasswordChar = true;
        }
        Class1 login = new Class1("DESKTOP-108SV7E\\SQLEXPRESS", "CS311-2022","itc311", "itc311"); 
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = login.GetData("SELECT * FROM tblAccounts WHERE username = '" + TxtUser.Text + "' AND password = '" + TxtPass.Text + "' AND status = 'ACTIVE'");
                if(dt.Rows.Count > 0 )
                {
                    frmMainWindow main = new frmMainWindow(TxtUser.Text, dt.Rows[0].Field<string>("userType"));
                    main.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect Username or Password or Account is INACTIVE", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message, "Error on log in button", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            TxtUser.Clear();
            TxtPass.Clear();
            TxtUser.Focus();
        }

        private void TxtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(Convert.ToChar(e.KeyChar) == 13)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
