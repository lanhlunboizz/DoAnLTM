using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using Newtonsoft.Json;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using Timer = System.Windows.Forms.Timer;


namespace Bai04
{
    public partial class Menu : Form
    {
        private Timer currentTimeTimer;

        public Menu()
        {
            InitializeComponent();
            currentTimeTimer = new Timer();
            currentTimeTimer.Interval = 1000; // 1 giây
            currentTimeTimer.Tick += CurrentTimeTimer_Tick;
            currentTimeTimer.Start();
        }

        private void CurrentTimeTimer_Tick(object sender, EventArgs e)
        {
            time_lb.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
        }

        private async void sh_down_button_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = false;
            label1.Visible = false;
            label2.Visible = true;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = "https://vtv.vn/lich-phat-song.htm";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string html = await response.Content.ReadAsStringAsync();

                        // Phân tích nội dung HTML và trích xuất thông tin chương trình
                        HtmlDocument document = new HtmlDocument();
                        document.LoadHtml(html);

                        // Trích xuất danh sách các ngày
                        var dateNodes = document.DocumentNode.SelectNodes("//ul[@class='date-selector']/li");
                        if (dateNodes == null)
                        {
                            MessageBox.Show("Không tìm thấy thông tin về các ngày.", "Lỗi");
                            return;
                        }

                        // Trích xuất danh sách các kênh
                        var channelNodes = document.DocumentNode.SelectNodes("//ul[@class='list-channel']/li");
                        if (channelNodes == null)
                        {
                            MessageBox.Show("Không tìm thấy thông tin về các kênh.", "Lỗi");
                            return;
                        }

                        // Tạo nội dung HTML tùy chỉnh
                        StringBuilder htmlBuilder = new StringBuilder();
                        htmlBuilder.Append("<html><body>");

                        // Thêm đoạn mã CSS để highlight chương trình
                        htmlBuilder.Append("<style>");
                        htmlBuilder.Append(".highlight { background-color: yellow; }");
                        htmlBuilder.Append("</style>");

                        // Danh sách thả xuống chọn ngày
                        htmlBuilder.Append("<select id='dateDropdown' onchange='filterShows()'>");
                        foreach (var dateNode in dateNodes)
                        {
                            var dateText = dateNode.SelectSingleNode(".//span[@class='date-month']").InnerText.Trim();
                            var date = DateTime.ParseExact(dateText, "dd/MM", CultureInfo.InvariantCulture);
                            if (date < DateTime.Today)
                            {
                                continue; // Bỏ qua các ngày đã qua
                            }

                            var dayOfWeek = dateNode.SelectSingleNode(".//span[@class='day']").InnerText.Trim();

                            htmlBuilder.Append($"<option value='{dateText}'>{dayOfWeek} - {dateText}</option>");
                        }
                        htmlBuilder.Append("</select>");

                        // Danh sách thả xuống chọn kênh
                        htmlBuilder.Append("<select id='channelDropdown' onchange='filterShows()'>");
                        int channelCount = 0; // Biến đếm số kênh
                        foreach (var channelNode in channelNodes)
                        {
                            var channelName = channelNode.SelectSingleNode(".//a").GetAttributeValue("title", "").Trim();
                            channelCount++; // Tăng biến đếm

                            htmlBuilder.Append($"<option value='{channelCount}'>{channelName}</option>");
                        }
                        htmlBuilder.Append("</select>");

                        // Thêm container để hiển thị các chương trình
                        htmlBuilder.Append("<div id='showsContainer'></div>");

                        // Thêm script để xử lý sự kiện thay đổi của dropdown và highlight chương trình
                        htmlBuilder.Append("<script>");
                        htmlBuilder.Append("function filterShows() {");
                        htmlBuilder.Append("var selectedDate = document.getElementById('dateDropdown').value;");
                        htmlBuilder.Append("var selectedChannel = document.getElementById('channelDropdown').value;");
                        htmlBuilder.Append("var shows = document.querySelectorAll('.show');");
                        htmlBuilder.Append("shows.forEach(function(show) {");
                        htmlBuilder.Append("var showDate = show.getAttribute('data-date');");
                        htmlBuilder.Append("var showChannel = show.getAttribute('data-channel');");
                        htmlBuilder.Append("if (showDate === selectedDate && showChannel === selectedChannel) {");
                        htmlBuilder.Append("show.style.display = 'block';");
                        htmlBuilder.Append("} else {");
                        htmlBuilder.Append("show.style.display = 'none';");
                        htmlBuilder.Append("}");
                        htmlBuilder.Append("});");
                        htmlBuilder.Append("}");
                        htmlBuilder.Append("var selectedShowTime = '';");
                        htmlBuilder.Append("function selectShow(element, time) {");
                        htmlBuilder.Append("var shows = document.querySelectorAll('.show');");
                        htmlBuilder.Append("shows.forEach(function(show) {");
                        htmlBuilder.Append("show.classList.remove('highlight');");
                        htmlBuilder.Append("});");
                        htmlBuilder.Append("element.classList.add('highlight');");
                        htmlBuilder.Append("selectedShowTime = time;");
                        htmlBuilder.Append("}");
                        htmlBuilder.Append("</script>");

                        // Lặp qua từng kênh để trích xuất thông tin chương trình
                        channelCount = 0; // Đặt lại biến đếm để sử dụng đúng cách trong vòng lặp trích xuất chương trình
                        foreach (var channelNode in channelNodes)
                        {
                            var channelName = channelNode.SelectSingleNode(".//a").GetAttributeValue("title", "").Trim();
                            channelCount++; // Tăng biến đếm

                            var programsNode = document.DocumentNode.SelectSingleNode($"//*[@id='wrapper']/ul[{channelCount}]"); // Sử dụng biến đếm kênh

                            if (programsNode != null)
                            {
                                // Trích xuất thông tin các chương trình
                                var programItems = programsNode.SelectNodes("./li");
                                if (programItems != null)
                                {
                                    foreach (var programItem in programItems)
                                    {
                                        var time = programItem.SelectSingleNode(".//span[@class='time']").InnerText.Trim();
                                        var title = programItem.SelectSingleNode(".//span[@class='title']").InnerText.Trim();
                                        var dateText = document.DocumentNode.SelectSingleNode(".//ul[@class='date-selector']//li[contains(@class, 'active')]//span[@class='date-month']").InnerText.Trim();

                                        // Tạo đoạn HTML cho chương trình này và thêm vào nội dung chung
                                        htmlBuilder.Append($"<div class='show' data-date='{dateText}' data-channel='{channelCount}' style='display:none;' onclick='selectShow(this, \"{time}\")'>");
                                        htmlBuilder.Append($"<h3>{time} - {title}</h3>");
                                        htmlBuilder.Append($"</div>");
                                    }
                                }
                            }
                        }

                        htmlBuilder.Append("</body></html>");

                        // Đảm bảo rằng WebView2 đã được khởi tạo
                        await webView21.EnsureCoreWebView2Async();

                        // Hiển thị nội dung HTML tùy chỉnh trong WebView2
                        webView21.NavigateToString(htmlBuilder.ToString());
                    }
                    else
                    {
                        MessageBox.Show("Lỗi: Không thể lấy trang web. Mã trạng thái: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }





        private void formSize_changed(object sender, EventArgs e)
        {
            // Lấy kích thước mới của form
            int newWidth = this.Width;
            int newHeight = this.Height;

            // Cập nhật kích thước của WebView2 để phù hợp với kích thước mới của form
            webView21.Size = new Size(newWidth, newHeight);

        }

        private async void alarm_button_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                // Lấy khung giờ đã chọn từ biến JavaScript
                string selectedShowTime = await webView21.ExecuteScriptAsync("selectedShowTime");

                // Xử lý chuỗi JSON từ JavaScript
                selectedShowTime = selectedShowTime.Trim('"');

                if (string.IsNullOrEmpty(selectedShowTime))
                {
                    MessageBox.Show("Bạn chưa chọn chương trình nào.", "Thông báo");
                    return;
                }

                // Chuyển đổi khung giờ sang DateTime
                DateTime showTime;
                if (!DateTime.TryParseExact(selectedShowTime, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out showTime))
                {
                    MessageBox.Show("Khung giờ không hợp lệ.", "Thông báo");
                    return;
                }

                DateTime currentTime = DateTime.Now;
                DateTime showDateTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, showTime.Hour, showTime.Minute, 0);

                // Kiểm tra thời gian chương trình có hợp lệ không (không phải thời gian đã qua)
                if (showDateTime <= currentTime)
                {
                    MessageBox.Show("Khung giờ đã qua, không thể đặt báo thức.", "Thông báo");
                    return;
                }

                // Tính thời gian còn lại để đặt báo thức
                TimeSpan timeUntilShow = showDateTime - currentTime;

                // Đặt báo thức
                Timer timer = new Timer();
                timer.Interval = (int)timeUntilShow.TotalMilliseconds;
                timer.Tick += (s, args) =>
                {
                    timer.Stop();
                    MessageBox.Show("Đến giờ xem chương trình rồi!", "Thông báo");
                };
                timer.Start();

                MessageBox.Show("Báo thức đã được đặt thành công.", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }



        private async void film_download_button_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            label2.Visible = false;
            progressBar1.Visible = false;
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
    }
}
