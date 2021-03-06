using System.Threading.Tasks;

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
    }
}
