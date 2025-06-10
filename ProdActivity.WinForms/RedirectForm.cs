using ProdActivity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProdActivity.WinForms
{
    public partial class RedirectForm : Form
    {
        private readonly User _currentUser;

        public RedirectForm(User user)
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeComponent();
            _currentUser = user;
        }
        private void RedirectForm_Load(object sender, EventArgs e)
        {


            btnOrderManagement.Visible = true;
            bool isAdmin = _currentUser.UserRoleId == 1;

            btnUserManagement.Visible = isAdmin;
            pcboxUser.Visible = isAdmin;
        }
        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            var form = new UserManagementForm();
            form.ShowDialog();
        }
        private void btnOrderManagement_Click(object sender, EventArgs e)
        {
            var form = new OrderManagementForm();
            form.ShowDialog();
        }
        private void btnTest_Click(object sender, EventArgs e)
        {
            var form = new MainForm();
            form.ShowDialog();
        }
    }
}
