using ProdActivity.DataAccess.Repositories;
using ProdActivity.DataAccess;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProdActivity.Entities;
using ProdActivity.Core;

namespace ProdActivity.WinForms
{
    public partial class UserManagementForm : Form
    {
        public UserManagementForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeComponent();
        }

        private void LoadUsers()
        {
            var repo = new UserRepository(new AppDbContext());
            var users = repo.GetAll();

            if (users == null || !users.Any())
            {
                dgvUsers.DataSource = null;
                MessageBox.Show("Kullanıcılar bulunamadı", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (chkActive.Checked && !chkPassive.Checked)
            {
                users = users.Where(u => u.IsActive).ToList();
            }
            else if (!chkActive.Checked && chkPassive.Checked)
            {
                users = users.Where(u => !u.IsActive).ToList();
            }

            // Arama
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                string search = txtSearch.Text.ToLower();
                users = users.Where(u => u.Name.ToLower().Contains(search)).ToList();
            }

            dgvUsers.DataSource = users.Select(u => new
            {
                u.Id,
                u.Name,
                Role = u.UserRole != null ? u.UserRole.Name : "Bulunamadı",
                Status = u.IsActive ? "Active" : "Passive"
            }).ToList();

            InitializeDataGridView(); 
        }



        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }
        private void UserManagementForm_Load(object sender, EventArgs e)
        {
            if (CurrentUser.User != null)
            {
                lblUsername.Text = CurrentUser.User.Name;
                lblRole.Text = CurrentUser.User.UserRole?.Name ?? "Rol Yok";
            }

            LoadUsers();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new UserEditForm(); 
            if (form.ShowDialog() == DialogResult.OK)
                LoadUsers();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow != null)
            {
                var id = (Guid)dgvUsers.CurrentRow.Cells["Id"].Value;
                var repo = new UserRepository(new AppDbContext());
                var user = repo.GetById(id);
                var form = new UserEditForm(user);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Logger.Info($"Kullanıcı düzenlendi: ID = {user.Id}, İsim = {user.Name}");
                    LoadUsers();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow != null)
            {
                var id = (Guid)dgvUsers.CurrentRow.Cells["Id"].Value;
                var repo = new UserRepository(new AppDbContext());
                var user = repo.GetById(id);

                var result = MessageBox.Show("Silmek istediğinize emin misiniz?", "Delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    repo.Delete(id);
                    Logger.Warning($"Kullanıcı silindi: ID = {user.Id}, İsim = {user.Name}");
                    LoadUsers();
                }
            }
        }

        private void InitializeDataGridView()
        {
            dgvUsers.EnableHeadersVisualStyles = false;

            // Başlıklar
            dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(34, 36, 49);
            dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvUsers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Satırlar
            dgvUsers.DefaultCellStyle.BackColor = Color.FromArgb(40, 42, 54);
            dgvUsers.DefaultCellStyle.ForeColor = Color.White;
            dgvUsers.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvUsers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(75, 75, 75);
            dgvUsers.DefaultCellStyle.SelectionForeColor = Color.White;

            // Alternatif satır
            dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(50, 52, 63);

            // Hücre kenarlıkları
            dgvUsers.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            // Arka plan
            dgvUsers.BackgroundColor = Color.FromArgb(48, 50, 61);
            dgvUsers.BorderStyle = BorderStyle.None;
            dgvUsers.GridColor = Color.Gray;

            // Otomatik sığdırma
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.RowTemplate.Height = 40;
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToResizeRows = false;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.MultiSelect = false;

            // Sütun hizalama
            foreach (DataGridViewColumn column in dgvUsers.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            foreach (DataGridViewRow row in dgvUsers.Rows)
            {
                if (row.Cells["Status"].Value != null)
                {
                    string status = row.Cells["Status"].Value.ToString();
                    var statusCell = row.Cells["Status"];

                    if (status == "Active")
                    {
                        statusCell.Style.BackColor = Color.Green; // Yeşil
                        statusCell.Style.ForeColor = Color.White;
                    }
                    else if (status == "Passive")
                    {
                        statusCell.Style.BackColor = Color.Red; // Kırmızı
                        statusCell.Style.ForeColor = Color.White;
                    }
                }
            }
        }


    }
}
