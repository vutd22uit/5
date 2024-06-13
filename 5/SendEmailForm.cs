using System;
using System.Windows.Forms;
using MimeKit;
using MailKit.Net.Smtp;
using System.IO;

namespace EmailClient
{
    public partial class SendEmailForm : Form
    {
        private SmtpClient _smtpClient;

        public SendEmailForm(SmtpClient smtpClient)
        {
            InitializeComponent();
            _smtpClient = smtpClient ?? throw new ArgumentNullException(nameof(smtpClient)); // Đảm bảo rằng _smtpClient không null
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtAttachment.Text = openFileDialog.FileName;
                }
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(txtName.Text, txtFrom.Text));
                message.To.Add(new MailboxAddress("", txtTo.Text));
                message.Subject = txtSubject.Text;

                var bodyBuilder = new BodyBuilder();
                if (chkHtml.Checked)
                {
                    bodyBuilder.HtmlBody = txtBody.Text;
                }
                else
                {
                    bodyBuilder.TextBody = txtBody.Text;
                }

                if (!string.IsNullOrEmpty(txtAttachment.Text) && File.Exists(txtAttachment.Text))
                {
                    bodyBuilder.Attachments.Add(txtAttachment.Text);
                }

                message.Body = bodyBuilder.ToMessageBody();

                await _smtpClient.SendAsync(message); // Đảm bảo _smtpClient đã được khởi tạo
                MessageBox.Show("Email đã được gửi!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gửi email thất bại: " + ex.Message);
            }
        }
    }
}
