using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.DetailViewModels
{
    /// <summary>
    /// Marks a type as requiring async initialization and provides the result of that intialization.
    /// </summary>
    public interface IDetailViewModel
    {
        /// <summary>
        /// The result of the asynchronous initialization of this instance.
        /// </summary>
        Task Initialization { get; }

        Command AddToListCommand { get; }

        Command AddToWatchListCommand { get; }

        Command AddToFavoritesCommand { get; }

        Command RateCommand { get; }
    }
}
