using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TestApp.Data.Models;
using TestApp.Data.Repositories;

namespace TestApp.ViewModels;

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

        for (int i = 0; i < RandomNumberBetween(5, 15); i++)
        {
            var Item = new Item($"Aname{i}", -1);
            await modelRepository.Create(Item);
        }

        var model = new DemoModel("" + DateTime.Now.Ticks);
        await modelRepository.Create(model);
        await modelRepository.Update(model.Id);
        await Read();
    }

    [RelayCommand]
    public async Task Read()
    {
        DemoModels.Clear();
        var models = await modelRepository.Read();
        foreach (var model in models)
        {
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

    private int RandomNumberBetween(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }


    public ObservableCollection<DemoModel> DemoModels { get; set; } = new();
}