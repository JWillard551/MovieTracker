namespace MovieTracker.App.ViewModels
{
    public class SearchResultViewModel : BaseViewModel
    {
        public string PhotoUrl
        {
            get
            {
                return "kingsman.jpg";
            }
        }

        public string TextValue { get; set; }

        public string ReleaseYear { get; set; }

        private RadialGaugeViewModel _ratingVM;
        public RadialGaugeViewModel RadialGaugeViewModel
        {
            get => _ratingVM;
            set => SetProperty(ref _ratingVM, value);
        }
    }
}
