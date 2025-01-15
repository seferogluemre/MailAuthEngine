using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailAuthEngine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DbMailActivationEntities context = new DbMailActivationEntities();

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            TblUser tblUser = new TblUser();
            Random random = new Random();
            int confirmCode = random.Next(100000, 1000000);

            tblUser.Name = TxtAd.Text;
            tblUser.Surname = TxtSoyad.Text;
            tblUser.Password = TxtPassword.Text;
            tblUser.isConfirm = false;
            tblUser.Email = TxtEmail.Text;
            tblUser.ConfirmCode = confirmCode.ToString();

            context.TblUser.Add(tblUser);
            context.SaveChanges();

                


        }
    }
}
