using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnLTM_GetInforUpcomingFilm
{
    public partial class MovieForm : Form
    {
        private readonly MovieService _movieService;
        private List<Movie> _movies;
        private Timer _timer;

        public MovieForm()
        {
            InitializeComponent();
            _movieService = new MovieService();
            _timer = new Timer();
            _timer.Interval = 1000; // cập nhật mỗi giây
            _timer.Tick += Timer_Tick;
        }

        private async void btdGetInforFilm_Click(object sender, EventArgs e)
        {
            string url = "https://betacinemas.vn/phim.htm";

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;

            try
            {
                // Lấy thông tin phim từ dịch vụ
                _movies = await _movieService.GetMoviesAsync(url);

                if (_movies == null || _movies.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin về các bộ phim.", "Lỗi");
                    return;
                }

                UpdateMovieDisplay();

                // Bắt đầu timer để cập nhật đếm ngược
                _timer.Start();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi");
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < _movies.Count; i++)
            {
                var movie = _movies[i];
                string countdown = movie.Countdown;
                string script = $"document.getElementById('countdown-{i}').innerText = 'Thời gian đếm ngược: {countdown}';";
                webView21.ExecuteScriptAsync(script);
            }
        }
        private void UpdateMovieDisplay()
        {
            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append("<html><body>");

            for (int i = 0; i < _movies.Count; i++)
            {
                var movie = _movies[i];
                htmlBuilder.Append("<div style='margin-bottom: 30px; border: 1px solid #ccc; padding: 10px;'>");
                htmlBuilder.Append("<div style='display: flex; flex-direction: row;'>");

                // Thêm hình ảnh phim
                htmlBuilder.Append("<div style='flex: 0 0 30%; margin-right: 20px;'>");
                htmlBuilder.AppendFormat("<img src='{0}' alt='{1}' style='max-width: 100%;'/>", movie.ImageUrl, movie.Title);
                htmlBuilder.Append("</div>");

                // Thêm thông tin phim
                htmlBuilder.Append("<div style='flex: 1;'>");
                htmlBuilder.AppendFormat("<h3><a href='{0}' target='_blank'>{1}</a></h3>", movie.RelativeMovieUrl, movie.Title);
                htmlBuilder.AppendFormat("<p>Thể loại: {0}</p>", movie.Genre);
                htmlBuilder.AppendFormat("<p>Thời lượng: {0}</p>", movie.Duration);
                htmlBuilder.AppendFormat("<p>Ngày công chiếu: {0}</p>", movie.ReleaseDate);

                // Thêm thông tin đếm ngược thời gian còn lại đến ngày chiếu phim
                htmlBuilder.AppendFormat("<p id='countdown-{0}'>Thời gian đếm ngược: {1}</p>", i, movie.Countdown);

                htmlBuilder.Append("</div>");
                htmlBuilder.Append("</div>");
                htmlBuilder.Append("</div>");
            }

            htmlBuilder.Append("</body></html>");

            // Đảm bảo rằng WebView2 đã được khởi tạo
            webView21.Invoke(new Action(async () =>
            {
                await webView21.EnsureCoreWebView2Async();
                // Hiển thị nội dung HTML tùy chỉnh trong WebView2
                webView21.NavigateToString(htmlBuilder.ToString());
            }));
        }

    }
}
