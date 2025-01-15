using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
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

            #region MailCodes
            MimeMessage mimeMessage = new MimeMessage();


            MailboxAddress mailboxAddressFrom = new MailboxAddress("AdminRegister", "emresefer53@outlook.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", TxtEmail.Text);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = "Email Adresinizin konfirmasyon kodu:" + confirmCode.ToString();
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            mimeMessage.Subject = "Email konfirnasyon kodu";

            MailKit.Net.Smtp.SmtpClient smtpClient = new MailKit.Net.Smtp.SmtpClient();
            smtpClient.Connect("smtp.gmail.com", 587, false);
            smtpClient.Authenticate("emresefer53@outlook.com", "YunusEmre53");
            smtpClient.Send(mimeMessage);
            smtpClient.Disconnect(true);

            MessageBox.Show("Mail adresinize konfirmasyon kodu gönderilmiştir");

            FrmMailConfirm frm = new FrmMailConfirm();
            frm.Show();

            #endregion
        }
    }
}
