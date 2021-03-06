using MovieTracker.Model.Client;
using MovieTracker.Model.ModelObjects;
using System.Threading;
using System.Threading.Tasks;

namespace MovieTracker.App.ViewModels.DetailViewModels
{
    public class ShowDetailViewModel : BaseViewModel, IDetailViewModel
    {
        public Task Initialization { get; private set; }
        public Show ShowInfo { get; set; }

        public ShowDetailViewModel(int id)
        {
            Initialization = InitializeAsync(id);
        }

        public async Task InitializeAsync(int id)
        {
            ShowInfo = await TMDbServiceClientHelper.GetShowDetailsById(id, new CancellationToken());
        }
    }
}
