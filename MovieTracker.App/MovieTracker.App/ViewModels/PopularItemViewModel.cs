using MovieTracker.Model.ModelObjects;

namespace MovieTracker.App.ViewModels
{
    public class PopularItemViewModel : BaseViewModel
    {
        public PopularItem PopularItem { get; set; }

        public PopularItemViewModel(PopularItem item)
        {
            PopularItem = item;
        }
    }
}
