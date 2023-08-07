using Avalonia.Controls.Templates;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AvaBudget.Models;

namespace AvaBudget.Views;

internal class BudgetItemTemplate : IDataTemplate
{
    public Control Build(object param) =>
        // TODO: build and return the control which represents your data
        // Sample: 
        // return new Textblock(){ Text = param.ToString() };
        new DataGrid
        {
            ItemsSource = param as ObservableCollection<BudgetItem>,
            AutoGenerateColumns = false,
            Columns =
            {
                new DataGridTextColumn
                {
                    Header="Date Due"
                }
            }
        };

    public bool Match(object data) =>
        // TODO: Check if the provided data matches or not. Return true if matches, otherwise false.
        // Sample: 
        // return data is string;
        data is ObservableCollection<BudgetItem>;
}