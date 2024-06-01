using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using MimeKit;
using MailKit.Net.Smtp;

namespace Bai04
{
    public partial class bookingT : Form
    {
        public bookingT(string movieTitle)
        {
            InitializeComponent();
            label4.Text = "Phim đã chọn: " + movieTitle;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên và email của bạn.", "Thông báo");
                return; // Dừng lại không gửi email nếu chưa nhập đủ thông tin
            }

            try
            {
                var client = new SmtpClient();
                client.Connect("smtp.gmail.com", 465, true); // smtp host, port, use ssl. 
                client.Authenticate("22520672@gm.uit.edu.vn", "alye jxdi auqm goua"); // gmail account, app password 

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Rạp chiếu phim", "22520672@gm.uit.edu.vn"));
                message.To.Add(new MailboxAddress("", textBox2.Text));
                message.Subject = "Đặt vé thành công!";
                message.Body = new TextPart("html") // gửi ở dạng plain text, hoặc có thể thay bằng “html” nếu muốn gửi email dạng html
                {
                    Text = "Cảm ơn quý khách " + textBox1.Text + " vì đã đặt vé!<br><br>" + label4.Text
                };

                client.Send(message);
                client.Disconnect(true);
                client.Dispose();

                // Thông báo thành công và đóng form
                MessageBox.Show("Đặt vé thành công!\nVui lòng kiểm tra email.", "Thông báo");
                this.Close();
            }
            catch (Exception ex)
            {
                // Thông báo lỗi nếu có
                MessageBox.Show("Có lỗi xảy ra khi gửi email: " + ex.Message, "Lỗi");
            }
        }
    } 
}
