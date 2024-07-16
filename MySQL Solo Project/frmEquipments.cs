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
    public partial class frmEquipments : Form
    {
        private string username;
        public frmEquipments(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        Class1 equipments = new Class1("DESKTOP-108SV7E\\SQLEXPRESS", "CS311-2022", "itc311", "itc311");
        private void frmEquipments_Load(object sender, EventArgs e)
        {
            dgvLoad();
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[7].Visible = false;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = equipments.GetData("SELECT assetNumber, serialNumber, Type, Branch, Status FROM tblEquipments WHERE assetNumber <> '" + username +
                    "'AND(assetNumber LIKE '%" + txtSearch.Text + "%' OR serialNumber LIKE '%" + txtSearch.Text + "%' OR Type LIKE '%" + txtSearch.Text + "%' OR Branch LIKE '%" + txtSearch.Text + "%') ORDER BY assetNumber ");
                dataGridView1.DataSource = dt;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on log in button", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void dgvLoad()
        {
            
            try
            {
                DataTable dt = equipments.GetData("SELECT assetNumber, serialNumber, Type, Manufacturer, yearModel, Description, Branch, Department, Status FROM tblEquipments WHERE assetNumber <> '" + username + "' ORDER BY dateCreated DESC");
                dataGridView1.DataSource = dt;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on log in button", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            frmAddEquipment createEquipment = new frmAddEquipment(username);
            createEquipment.Show();
        }
        private int rowSelected;
        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            rowSelected = dataGridView1.CurrentCell.RowIndex;
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string editAssetNumber, editSerialNumber, editType, editManufacture, editYearModel, editDescription, editBranch, editDepartment, editStatus;

            editAssetNumber = dataGridView1.Rows[rowSelected].Cells[0].Value.ToString();
            editSerialNumber = dataGridView1.Rows[rowSelected].Cells[1].Value.ToString();
            editType = dataGridView1.Rows[rowSelected].Cells[2].Value.ToString();
            editManufacture = dataGridView1.Rows[rowSelected].Cells[3].Value.ToString();
            editYearModel = dataGridView1.Rows[rowSelected].Cells[4].Value.ToString();
            editDescription = dataGridView1.Rows[rowSelected].Cells[5].Value.ToString();
            editBranch = dataGridView1.Rows[rowSelected].Cells[6].Value.ToString();
            editDepartment = dataGridView1.Rows[rowSelected].Cells[7].Value.ToString();
            editStatus = dataGridView1.Rows[rowSelected].Cells[8].Value.ToString();
            frmUpdateEquipment updateEquipment = new frmUpdateEquipment(editAssetNumber, editSerialNumber, editType, editManufacture, editYearModel, editDescription, editBranch, editDepartment, editStatus, username);
            updateEquipment.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult answer = MessageBox.Show("Are you sure you want to delete this data?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    equipments.executeSQL("DELETE FROM tblEquipments WHERE assetNumber ='" + dataGridView1.Rows[rowSelected].Cells[0].Value.ToString() + "' ");
                    if (equipments.rowAffected > 0)
                    {
                        MessageBox.Show("Equipment deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        equipments.executeSQL("INSERT INTO tblDeleteLogsEquipment VALUES('" + DateTime.Now.ToString("MM/dd/yyyy ") + "', '" + DateTime.Now.ToLongTimeString() + "', 'Equipment Management', '" + dataGridView1.Rows[rowSelected].Cells[0].Value.ToString() + "', '" + username + "')");
                        frmEquipments newFrm = (frmEquipments)Application.OpenForms["frmEquipments"];
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
            frmEquipments_Load(sender, e);
        }
    }
}
