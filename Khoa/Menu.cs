using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using Newtonsoft.Json;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace Bai04
{
    public partial class Menu : Form
    {
       

        public Menu()
        {
            InitializeComponent();
            label1.Visible = false;
        }

        public async void button1_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            string url = "https://betacinemas.vn/phim.htm";

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;

            try
            {
                // Lấy nội dung HTML của trang web
                using HttpClient client = new HttpClient();
                string htmlContent = await client.GetStringAsync(url);

                // Phân tích HTML với HtmlAgilityPack
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(htmlContent);

                // Trích xuất thông tin phim
                var movieNodes = document.DocumentNode.SelectNodes("//div[contains(@class, 'item')]");
                if (movieNodes == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin về các bộ phim.", "Lỗi");
                    return;
                }

                // Tạo nội dung HTML tùy chỉnh
                StringBuilder htmlBuilder = new StringBuilder();
                htmlBuilder.Append("<html><body>");

                // Thiết lập giá trị tối đa của ProgressBar dựa trên số lượng phim
                progressBar1.Maximum = movieNodes.Count;
                int currentProgress = 0;


                int i = 1;
                for (int j = 0; j < movieNodes.Count; j++)
                {
                    // Lấy node hiện tại bằng cách sử dụng chỉ số của nó trong movieNodes
                    var node = movieNodes[j];

                    // Trích xuất URL hình ảnh phim
                    var imageNode = node.SelectSingleNode(".//img");
                    string imageUrl = imageNode?.GetAttributeValue("src", string.Empty) ?? string.Empty;

                    // Sử dụng giá trị của i trong SelectSingleNode để tìm liên kết phim

                    var movieLinkNode = node.SelectSingleNode($"//*[@id='tab-1']/div/div[{i}]/div/div[2]/div[1]/h3/a");
                    i++;
                    // Trích xuất tiêu đề phim từ liên kết phim
                    string title = movieLinkNode?.InnerText.Trim() ?? "Không có tiêu đề";

                    // Trích xuất URL chi tiết phim từ liên kết phim
                    string relativeMovieUrl = movieLinkNode?.GetAttributeValue("href", string.Empty) ?? string.Empty;

                    // Tạo đường dẫn URL chi tiết của phim bằng cách ghép nối tên miền với đường dẫn tương đối
                    string baseUrl = "https://betacinemas.vn";
                    string movieUrl = baseUrl + relativeMovieUrl;


                    // Thêm đoạn mã CSS vào phần head của tài liệu HTML
                    htmlBuilder.Append("<head>");
                    htmlBuilder.Append("<style>");
                    htmlBuilder.Append(".highlighted-layout {");
                    htmlBuilder.Append("border: 2px solid blue; /* Thay đổi màu viền khi layout được chọn */");
                    htmlBuilder.Append("background-color: #42bde3; /* Thay đổi màu nền khi layout được chọn */");
                    htmlBuilder.Append("}");
                    htmlBuilder.Append("</style>");
                    htmlBuilder.Append("</head>");

                    // Tạo nội dung HTML cho mỗi phim
                    htmlBuilder.Append("<div style='margin-bottom: 30px; border: 1px solid #ccc; padding: 10px;' ");
                    htmlBuilder.Append("onclick='highlightAndSelectLayout(this)'>"); // Thêm sự kiện click cho việc chọn layout
                    htmlBuilder.Append("<div style='display: flex; flex-direction: row;'>");

                    // Thêm hình ảnh phim
                    htmlBuilder.Append("<div style='flex: 0 0 30%; margin-right: 20px;'>");
                    htmlBuilder.AppendFormat("<img src='{0}' alt='{1}' style='max-width: 100%;'/>", imageUrl, title);
                    htmlBuilder.Append("</div>");

                    // Thêm thông tin phim
                    htmlBuilder.Append("<div style='flex: 1;'>");
                    htmlBuilder.AppendFormat("<h3><a href='{0}' target='_blank'>{1}</a></h3>", movieUrl, title);
                    // Thêm thông tin khác về phim ở đây (ví dụ: thể loại, ngày công chiếu, mô tả, v.v.)
                    htmlBuilder.Append("</div>");

                    htmlBuilder.Append("</div>");
                    htmlBuilder.Append("</div>");

                    htmlBuilder.Append("<script>");
                    htmlBuilder.Append("var selectedMovieTitle = '';"); // Biến toàn cục để lưu trữ tiêu đề phim
                    htmlBuilder.Append("function highlightAndSelectLayout(layout) {");
                    htmlBuilder.Append("var highlightedLayouts = document.querySelectorAll('.highlighted-layout');");
                    htmlBuilder.Append("for (var i = 0; i < highlightedLayouts.length; i++) {");
                    htmlBuilder.Append("highlightedLayouts[i].classList.remove('highlighted-layout');");
                    htmlBuilder.Append("}");
                    htmlBuilder.Append("layout.classList.add('highlighted-layout');");
                    htmlBuilder.Append("selectedLayoutHtml = layout.innerHTML;"); // Lưu trữ nội dung của layout được chọn

                    // Lấy tiêu đề phim từ layout được chọn
                    htmlBuilder.Append("selectedMovieTitle = layout.querySelector('h3 a').innerText;");
                    htmlBuilder.Append("}");
                    htmlBuilder.Append("</script>");



                    // Cập nhật tiến trình
                    currentProgress++;
                    progressBar1.Value = currentProgress;
                }


                htmlBuilder.Append("</body></html>");

                // Đảm bảo rằng WebView2 đã được khởi tạo
                await webView21.EnsureCoreWebView2Async();

                // Hiển thị nội dung HTML tùy chỉnh trong WebView2
                webView21.NavigateToString(htmlBuilder.ToString());

                // Hoàn thành tiến trình
                progressBar1.Value = progressBar1.Maximum;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi");
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            // Lấy tiêu đề phim đã chọn từ JavaScript
            var result = await webView21.ExecuteScriptAsync("selectedMovieTitle");
            var selectedTitle = result.Trim('"'); // Loại bỏ dấu ngoặc kép

            if (!string.IsNullOrEmpty(selectedTitle))
            {
                // Truyền tiêu đề phim qua form mới
                bookingT form = new bookingT(selectedTitle);
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một bộ phim.", "Thông báo");
            }
        }
    }
}
