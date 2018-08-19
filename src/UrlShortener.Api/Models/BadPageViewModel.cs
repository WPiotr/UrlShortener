namespace UrlShortener.Api.Models
{
    public class BadPageViewModel
    {
        public string ShortPath { get; }
        public string ErrorMessage { get; }

        public BadPageViewModel(string shortPath, string errorMessage)
        {
            ShortPath = shortPath;
            ErrorMessage = errorMessage;
        }
    }
}