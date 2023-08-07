using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaBudget.Models;

public partial class Category : ObservableValidator, INotifyCollectionChanged
{
    [ObservableProperty]
    private Category? _rootCategory;

    [ObservableProperty, Required]
    private string _categoryName;

    [ObservableProperty]
    private string? _description;

    [ObservableProperty]
    private bool _isDeposit = false;

    public Category(Category? rootCategory, string categoryName, string? description = null, bool isDeposit=false)
    {
        RootCategory = rootCategory;
        CategoryName = categoryName ?? throw new ArgumentNullException(nameof(categoryName));
        IsDeposit = isDeposit;
    }

    public event NotifyCollectionChangedEventHandler? CollectionChanged
    {
        add => ((INotifyCollectionChanged)Children).CollectionChanged += value;
        remove => ((INotifyCollectionChanged)Children).CollectionChanged -= value;
    }

    [ObservableProperty]
    private ObservableCollection<Category> _children = new();
}
