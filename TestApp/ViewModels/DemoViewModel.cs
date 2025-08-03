using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TestApp.Data.Models;
using TestApp.Data.Repositories;

namespace TestApp.ViewModels
{
    public partial class DemoViewModel : ObservableObject
    {
        private readonly DemoModelRepository modelRepository;

        public DemoViewModel(DemoModelRepository modelRepository)
        {
            DemoModels = new ObservableCollection<DemoModel>();
            this.modelRepository = modelRepository;
        }

        [RelayCommand]
        public async Task Save()
        {
            DemoModels.Clear();
            var model = new DemoModel("Test Model " + DateTime.Now.Ticks);
            for (int i = 1; i < 16; i++)
            {                               
                var Item = new Item($"Aname{i}");
                model.Items.Add(Item);
            }
            await modelRepository.Create(model);
            await Read();
        }

        [RelayCommand]
        public async Task Read()
        {
            DemoModels.Clear();
            var models = await modelRepository.Read();
            foreach (var model in models)
            {
                Debug.WriteLine($"Model: {model.Name}, Items Count: {model.Items.Count}");
                foreach (var item in model.Items)
                {
                    Debug.WriteLine($"  Item: {item.Name}");
                }
                DemoModels.Add(model);
            }
        }

        [RelayCommand]
        public async Task ClearDB()
        {                        
            foreach (var item in DemoModels)
            {
                await modelRepository.Delete(item.Id);
            }
            DemoModels.Clear();           
        }

        public ObservableCollection<DemoModel> DemoModels { get; set; } = new();
    }
}