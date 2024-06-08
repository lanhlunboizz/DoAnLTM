using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnLTM_GetInforUpcomingFilm
{
    public class Movie
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Genre { get; set; }
        public string Duration { get; set; }
        public string ReleaseDate { get; set; }
        public string RelativeMovieUrl { get; set; }

        public DateTime? ReleaseDateTime
        {
            get
            {
                if (DateTime.TryParse(ReleaseDate, out var releaseDateTime))
                {
                    return releaseDateTime;
                }
                return null;
            }
        }

        public string Countdown
        {
            get
            {
                if (ReleaseDateTime.HasValue)
                {
                    var timeRemaining = ReleaseDateTime.Value - DateTime.Now;
                    if (timeRemaining.TotalDays > 0)
                    {
                        return $"{(int)timeRemaining.TotalDays} ngày {timeRemaining.Hours} giờ {timeRemaining.Minutes} phút {timeRemaining.Seconds} giây";
                    }
                    else
                    {
                        return "Đồng hồ đếm ngược đã kết thúc!";
                    }
                }
                return "Không có dữ liệu cho ngày công chiếu";
            }
        }

    }
}
