using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TestApp.Data.Repositories;
using TestApp.Models;

namespace TestApp.ViewModels
{
    public partial class DemoViewModel : ObservableObject
    {

        private readonly DemoModelRepository modelRepository;

        public DemoViewModel(DemoModelRepository modelRepository)
        {
            Models = new List<DemoModel>();
            this.modelRepository = modelRepository;
        }

        [RelayCommand]
        public async Task Save()
        {
            for (int i = 0; i < 15; i++)
                await modelRepository.Create(new DemoModel("Test Model " + DateTime.Now.Ticks));           
            await Read();
        }

        [RelayCommand]
        public async Task Read()
        {
            Models.Clear();
            Models = await modelRepository.Read();
        }

        [RelayCommand]
        public async Task ClearDB()
        {                        
            foreach (var item in Models)
            {
                await modelRepository.Delete(item.Id);
            }
            Models.Clear();           
        }

        [ObservableProperty]
        private List<DemoModel> models;
    }
}