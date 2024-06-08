using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoAnLTM_GetInforUpcomingFilm
{
    public class MovieService
    {
        private readonly MovieRepository _repository;

        public MovieService()
        {
            _repository = new MovieRepository();
        }

        public async Task<List<Movie>> GetMoviesAsync(string url)
        {
            string htmlContent = await _repository.GetHtmlContentAsync(url);
            return _repository.ExtractMoviesFromHtml(htmlContent);
        }
    }
}
