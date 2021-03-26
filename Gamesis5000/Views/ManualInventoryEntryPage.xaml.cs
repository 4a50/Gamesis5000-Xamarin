using Gamesis5000.ViewModels;
using Gamesis5000.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace Gamesis5000.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ManualInventoryEntryPage : ContentPage
  {
    readonly ManualInventoryEntryViewModel _vm;
    private Game game;
    public ManualInventoryEntryPage()
    {
      InitializeComponent();
      game = new Game();
      BindingContext = _vm = new ManualInventoryEntryViewModel();
      Title = "Manual Inventory Entry";
      
    }
    /// <summary>
    /// Event Trigger that will capture the data from an Entry Field when user is completed.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Entry_Completed(object sender, EventArgs e)
    {
      Entry entryItem = sender as Entry;
      string text = ((Entry)sender).Text;
      Debug.WriteLine($"[Dev Note] StyleId: {entryItem.StyleId} Captured: {text}");
      _vm.EntryUpdateModel(entryItem.StyleId, text, DateTime.Now);
    }
    /// <summary>
    /// Will Capture each character as it is entered in the field.  Not Currently Used in this app
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
      string text = ((Entry)sender).Text;
      Debug.WriteLine($"[Dev Note] " + text);
    }

    private void OnSaveButtonClicked(object sender, EventArgs e)
    {
      Debug.WriteLine("[Dev Note] Save Button Clicked.");
      Task.Run(async () => await _vm.TextEntryIntoDataBase());
      //Task.Run(async() => await Shell.Current.GoToAsync("//StartPage"));
    }

    private void ClearAllValues(object sender, EventArgs e)
    {

    }
  }
}