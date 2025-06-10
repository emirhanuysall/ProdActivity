using ProdActivity.DataAccess.Repositories;
using ProdActivity.DataAccess;
using ProdActivity.Entities;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ProdActivity.WinForms
{
    public partial class UserEditForm : Form
    {
        private readonly UserRepository _userRepository;
        private readonly UserRoleRepository _roleRepository;
        private User _currentUser;


        public UserEditForm(User user = null)
        {
            InitializeComponent();
            _userRepository = new UserRepository(new AppDbContext());
            _roleRepository = new UserRoleRepository(new AppDbContext());
            _currentUser = user;
        }


        private void UserEditForm_Load(object sender, EventArgs e)
        {
            var roles = _roleRepository.GetAll();
            cmbRole.DataSource = roles;
            cmbRole.DisplayMember = "Name";
            cmbRole.ValueMember = "Id";

            if (_currentUser != null)
            {
                txtName.Text = _currentUser.Name;
                cmbRole.SelectedValue = _currentUser.UserRoleId;
                chkIsActive.Checked = _currentUser.IsActive;
                txtPassword.Text = ""; // güvenlik için boş gösteriyoruz
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            var name = txtName.Text;
            var roleId = (int)cmbRole.SelectedValue;
            var isActive = chkIsActive.Checked;
            var password = txtPassword.Text;

            if (_currentUser == null)
            {
                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    UserRoleId = roleId,
                    IsActive = isActive,
                    Password = password
                };

                _userRepository.Add(newUser);
            }
            else
            {
                _currentUser.Name = name;
                _currentUser.UserRoleId = roleId;
                _currentUser.IsActive = isActive;
                _currentUser.Password = password;

                _userRepository.Update(_currentUser);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

    }
}
