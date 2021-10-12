using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HumanResourceManagement
{
    public partial class FrmEmployeeDetail : Form
    {
        public FrmEmployeeDetail()
        {
            InitializeComponent();
        }

        void ReloadForm()
        {
            dgvEmployee.DataSource = null;
            dgvEmployee.DataSource = list;
            dgvEmployee.Columns[0].HeaderText = "Employee ID";
            dgvEmployee.Columns[0].ReadOnly = true;
            dgvEmployee.Columns[1].HeaderText = "Name";
            dgvEmployee.Columns[1].ReadOnly = true;
            dgvEmployee.Columns[2].HeaderText = "Phone";
            dgvEmployee.Columns[2].ReadOnly = true;
            dgvEmployee.Columns[3].HeaderText = "Gender";
            dgvEmployee.Columns[3].ReadOnly = true;
            dgvEmployee.Columns[4].HeaderText = "Degree";
            dgvEmployee.Columns[4].ReadOnly = true;
        }

        Employee checkExist(Employee emp)
        {
            foreach(var e in list)
            {
                if (e.EmployeeID.Equals(emp.EmployeeID)) return e;
            }
            return null;
        }

        bool ValidForm()
        {
            bool flag = true;
            string strError = "";

            string empID = txtEmployeeID.Text.Trim();
            string empName = txtEmployeeName.Text.Trim();
            string degree = cboDegree.SelectedIndex != -1 ? cboDegree.SelectedItem.ToString() : "";

            Regex regEmpId = new Regex(@"^E\d{4}$");
            if (!regEmpId.IsMatch(empID))
            {
                flag = false;
                strError += "Employee ID is invalid\n";
                txtEmployeeID.Focus();
            }

            Regex regEmpName = new Regex(@"^[\w\s]+$");
            if (!regEmpName.IsMatch(empName))
            {
                flag = false;
                strError += "Employee Name is invalid\n";
                txtEmployeeID.Focus();
            }

            if(degree == "")
            {
                flag = false;
                strError += "Degree must be selected";
            }

            if(!flag) MessageBox.Show(strError);
            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // neu du lieu tren form la hop le thi hien thi du lieu tren form, neu khong thi
            if (ValidForm())
            {
                //string str = "";
                //str += "Emp ID: " + txtEmployeeID.Text.Trim() + "\n";
                //str += "Emp name: " + txtEmployeeName.Text.Trim() + "\n";
                //str += "Phone: " + mtxtPhone.Text.Trim() + "\n";
                //str += "Gender: " + (rbMale.Checked ? "Male" : "Female") +"\n";
                //str += "Degree: " + cboDegree.SelectedItem.ToString();
                //MessageBox.Show(str);

                Employee emp = new Employee() { EmployeeID = txtEmployeeID.Text.Trim(), 
                                                EmployeeName = txtEmployeeName.Text.Trim(), 
                                                Phone = mtxtPhone.Text.Trim(), 
                                                Gender = (rbMale.Checked ? "Male" : "Female"), 
                                                Degree = cboDegree.SelectedItem.ToString()};
                
                if (checkExist(emp) == null)
                {
                    list.Add(emp);
                    txtEmployeeID.Clear();
                    txtEmployeeName.Clear();
                    mtxtPhone.Clear();
                    rbMale.Checked = true;
                    rbFemale.Checked = false;
                    cboDegree.ResetText();
                }
                else
                {
                    Employee updateEmp = checkExist(emp);
                    updateEmp.EmployeeName = txtEmployeeName.Text.Trim();
                    updateEmp.Phone = mtxtPhone.Text.Trim();
                    updateEmp.Gender = rbMale.Checked ? "Male" : "Female";
                    updateEmp.Degree = cboDegree.SelectedItem.ToString();
                    txtEmployeeID.Clear();
                    txtEmployeeName.Clear();
                    mtxtPhone.Clear();
                    rbMale.Checked = true;
                    rbFemale.Checked = false;
                    cboDegree.ResetText();
                    btnSave.Text = "Save";
                }
                ReloadForm();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            // neu muon thoat het cac cua so kha, Application.Exit()
        }

        List<Employee> list = new List<Employee>();

        private void FrmEmployeeDetail_Load(object sender, EventArgs e)
        {
            list.Add(new Employee() { EmployeeID = "E0001", EmployeeName = "Nguyen Long", Phone = "096-3487529", Gender = "Male", Degree = "Engineer" });
            ReloadForm();
        }

        private void dgvEmployee_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtEmployeeID.Text = this.dgvEmployee.CurrentRow.Cells[0].Value.ToString();
            txtEmployeeName.Text = this.dgvEmployee.CurrentRow.Cells[1].Value.ToString();
            mtxtPhone.Text = this.dgvEmployee.CurrentRow.Cells[2].Value.ToString();
            String gender = this.dgvEmployee.CurrentRow.Cells[3].Value.ToString();
            if (gender == "Male") rbMale.Checked = true;
            else rbFemale.Checked = true;
            cboDegree.SelectedItem = this.dgvEmployee.CurrentRow.Cells[4].Value.ToString();
            btnSave.Text = "Update";
        }
    }

    class Employee
    {
        public string EmployeeID { get; set; }

        public string EmployeeName { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Degree { get; set; }
    }

}
