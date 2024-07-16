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
    public partial class frmTicketTechnicalComplete : Form
    {
        private string TicketNumber, Problem, Details, createdBy, date, completedBy;

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public frmTicketTechnicalComplete(string ticketnumber, string Problem, string Details, string createdBy, string date, string completedBy)
        {
            InitializeComponent();
            this.TicketNumber = ticketnumber;
            this.Problem = Problem;
            this.Details = Details;
            this.createdBy = createdBy;
            this.date = date;
            this.completedBy = completedBy;
        }
        Class1 completeTicket = new Class1("DESKTOP-108SV7E\\SQLEXPRESS", "CS311-2022", "itc311", "admin");
        private void frmTicketTechnicalComplete_Load(object sender, EventArgs e)
        {
            TxtTicket.Text = TicketNumber;
            txtProblem.Text = Problem;
            txtDetails.Text = Details;
            txtCreatedBy.Text = createdBy;
            txtDateCreated.Text = date;
        }
        private void btnComplete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult answer = MessageBox.Show("Are you sure you want to complete this Ticket?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    completeTicket.executeSQL("UPDATE tblTicketUser SET Status = 'WAITING FOR APPROVAL', completedBy = '" + completedBy.ToUpper() +
                            "', dateCompleted = '" + DateTime.Now.ToString("dd-MM-yyyy") + "' WHERE TicketNumber = '" + TxtTicket.Text + "' ");
                    if (completeTicket.rowAffected > 0)
                    {
                        MessageBox.Show("Ticket Complete!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmTicketTechnical newFrm = (frmTicketTechnical)Application.OpenForms["frmTicketTechnical"];
                        newFrm.dgvLoad();
                        this.Close();
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Complete Ticket!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


