using MovieTracker.Model.Client;
using MovieTracker.Model.ModelObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.DetailViewModels
{
    public class PersonDetailViewModel : BaseViewModel, IDetailViewModel
    {
        public Task Initialization { get; private set; }
        public Person PersonInfo { get; set; }

        public Command AddToListCommand => throw new NotImplementedException();

        public Command AddToWatchListCommand => throw new NotImplementedException();

        public Command AddToFavoritesCommand => throw new NotImplementedException();

        public Command RateCommand => throw new NotImplementedException();

        public PersonDetailViewModel(int id)
        {
            Initialization = InitializeAsync(id);
        }

        public async Task InitializeAsync(int id)
        {
            PersonInfo = await TMDbServiceClientHelper.GetPersonDetailsById(id, new CancellationToken());
        }
    }
}
