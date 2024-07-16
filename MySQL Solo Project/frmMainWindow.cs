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
    public partial class frmMainWindow : Form
    {
        private string userName, userType;
        public frmMainWindow(string userName, string userType)
        {
            InitializeComponent();
            label2.Text =  userName.First().ToString().ToUpper() + userName.Substring(1);
            label3.Text =  userType;
            label4.Text = userName.First().ToString().ToUpper() + userName.Substring(1) + " Dashboard";
            this.userName = userName;
            this.userType = userType;
        }
        bool sidebarExpand;
        private bool isCollapsed, isCollapsed1;
        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width == sidebar.MinimumSize.Width)
                {
                    panel7.Visible = false;
                    sidebarExpand = false;
                    sidebarTimer.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width == sidebar.MaximumSize.Width)
                {
                    panel7.Visible = true;
                    sidebarExpand = true;
                    sidebarTimer.Stop();
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(isCollapsed)
            {
                panelDrop.Height += 10;
                if(panelDrop.Size == panelDrop.MaximumSize)
                {
                    timer1.Stop();
                    isCollapsed = false;
                }
            }
            else
            {
                panelDrop.Height -= 10;
                if (panelDrop.Size == panelDrop.MinimumSize)
                {
                    timer1.Stop();
                    isCollapsed = true;
                }
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (isCollapsed1)
            {
                panelDrop2.Height += 10;
                if (panelDrop2.Size == panelDrop2.MaximumSize)
                {
                    timer2.Stop();
                    isCollapsed1 = false;
                }
            }
            else
            {
                panelDrop2.Height -= 10;
                if (panelDrop2.Size == panelDrop2.MinimumSize)
                {
                    timer2.Stop();
                    isCollapsed1 = true;
                }
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void menu_Click_1(object sender, EventArgs e)
        {
            sidebarTimer.Start();
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Close();
        }

        private void frmMainWindow_Load(object sender, EventArgs e)
        {
            if (userType == "USER")
            {
                btnAccount.Visible = false;
                btnEquipment.Visible = false;
                btnTicket.Visible = true;
                panelDrop2.Visible = false;
            }
            if (userType == "TECHNICAL")
            {
                btnAccount.Visible = false;
                btnEquipment.Visible = true;
                btnTicket.Visible = true;
                panelDrop2.Visible = true;
            }
            if (userType == "ADMINISTRATOR")
            {
                btnAccount.Visible = true;
                btnEquipment.Visible = true;
                btnTicket.Visible = true;
                panelDrop2.Visible = true;
                button16.Visible = true;
            }
        }

        private void btnEquipment_Click(object sender, EventArgs e)
        {
            frmEquipments equipments = new frmEquipments(userName);
            equipments.Show();
        }

        private void btnTicket_Click(object sender, EventArgs e)
        {
            if(userType == "USER")
            {
                frmTicketsUser tickets = new frmTicketsUser(userName);
                tickets.Show();
            }
            if (userType == "ADMINISTRATOR")
            {
                frmTicketAdmin tickets = new frmTicketAdmin(userName);
                tickets.Show();
            }
            if (userType == "TECHNICAL")
            {
                frmTicketTechnical tickets = new frmTicketTechnical(userName);
                tickets.Show();
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            frmDeleteAccountLogs accounts = new frmDeleteAccountLogs(userName);
            accounts.Show();
        }

        private void btnDeleteEquip_Click(object sender, EventArgs e)
        {
            frmDeleteEquipmentLogs equipment = new frmDeleteEquipmentLogs(userName);
            equipment.Show();
        }

        private void btnDeleteTicket_Click(object sender, EventArgs e)
        {
            tblDeleteLogsTicket ticket = new tblDeleteLogsTicket(userName);
            ticket.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmAbout about = new frmAbout();
            about.Show();
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            frmAccounts accounts = new frmAccounts(userName);
            accounts.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }             
    }
}
