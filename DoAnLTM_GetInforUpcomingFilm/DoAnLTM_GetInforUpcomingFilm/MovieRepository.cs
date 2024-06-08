using HtmlAgilityPack;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace DoAnLTM_GetInforUpcomingFilm
{
    public class MovieRepository
    {
        private readonly HttpClient _client;

        public MovieRepository()
        {
            _client = new HttpClient();
        }

        public async Task<string> GetHtmlContentAsync(string url)
        {
            return await _client.GetStringAsync(url);
        }

        public List<Movie> ExtractMoviesFromHtml(string htmlContent)
        {
            var movies = new List<Movie>();
            var document = new HtmlDocument();
            document.LoadHtml(htmlContent);

            var movieNodes = document.DocumentNode.SelectNodes("//div[contains(@class, 'item')]");
            if (movieNodes != null)
            {
                int i = 1;

                foreach (var node in movieNodes)
                {
                    //.//div[1]/div/div[2]/div[1]/ul/li[1]/span
                    var imageNode = node.SelectSingleNode(".//img");
                    var movieLinkNode = node.SelectSingleNode($"//*[@id='tab-1']/div/div[{i}]/div/div[2]/div[1]/h3/a"); 
                    var genreNode = node.SelectSingleNode($"//*[@id='tab-1']/div/div[{i}]/div/div[2]/div[1]/ul/li[1]/text()");
                    var durationNode = node.SelectSingleNode($"//*[@id='tab-1']/div/div[{i}]/div/div[2]/div[1]/ul/li[2]/text()");
                    //var releaseDateNode = node.SelectSingleNode($"//*[@id='tab-1']/div/div[{i}]/div/div[2]/div[1]/ul/li[2]/text()");

                    i++;

                    var movie = new Movie
                    {
                        ImageUrl = imageNode?.GetAttributeValue("src", string.Empty) ?? string.Empty,
                        Title = movieLinkNode?.InnerText.Trim() ?? "Không có thông tin",
                        RelativeMovieUrl = movieLinkNode?.GetAttributeValue("href", string.Empty) ?? string.Empty,
                        Genre = genreNode?.InnerText.Trim() ?? "Không có thông tin",
                        Duration = durationNode?.InnerText.Trim() ?? "Không có thông tin",
                        //ReleaseDate = releaseDateNode?.InnerText.Trim() ?? "Không có thông tin"
                    };

                    movies.Add(movie);
                }
            }
            return movies;
        }
    }
}

