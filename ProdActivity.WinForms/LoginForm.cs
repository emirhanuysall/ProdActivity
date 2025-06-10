using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProdActivity.DataAccess.Repositories;
using ProdActivity.Entities;
using ProdActivity.DataAccess;
using ProdActivity.WinForms;

namespace ProdActivity.UI
{
    public partial class LoginForm : Form
    {
        private readonly UserRepository _userRepository;

        public LoginForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;


            InitializeComponent();
            _userRepository = new UserRepository(new AppDbContext());

            txtPassword.PasswordChar = '*';
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş bırakılamaz.");
                return;
            }

            User? user = _userRepository.Login(username, password);

            if (user != null)
            {
                CurrentUser.User = user;

                MessageBox.Show($"Giriş başarılı. Hoş geldiniz: {user.Name} ({user.UserRole?.Name ?? "Rol Tanımsız"})");

                new RedirectForm(user).Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı.");
            }
        }
    }
}
