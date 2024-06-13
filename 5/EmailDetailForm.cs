using System;
using System.Windows.Forms;
using MimeKit;

namespace EmailClient
{
    public partial class EmailDetailForm : Form
    {
        private MimeMessage _message;

        public EmailDetailForm(MimeMessage message)
        {
            InitializeComponent();
            _message = message;
            LoadEmailDetails();
        }

        private void LoadEmailDetails()
        {
            txtFrom.Text = string.Join(", ", _message.From);
            txtTo.Text = string.Join(", ", _message.To);
            txtSubject.Text = _message.Subject;
            txtBody.Text = _message.TextBody;
        }

        private void btnReply_Click(object sender, EventArgs e)
        {
            // Code to handle reply functionality can be added here
        }
    }
}
