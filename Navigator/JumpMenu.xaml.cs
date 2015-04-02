namespace Navigator
{
  using System.Diagnostics;
  using System.Windows;
  using System.Windows.Controls;
  using System.Windows.Input;
  using ProviderModel;
  using KeyEventArgs = System.Windows.Input.KeyEventArgs;
  using TextBox = System.Windows.Controls.TextBox;
  using System;

  /// <summary>
  /// Interaction logic for JumpMenu.xaml
  /// </summary>
  public partial class JumpMenu : Window
  {
    public JumpMenu()
    {
      InitializeComponent();
    }

    public void FocusInput()
    {
      tbInput.Focus();
    }

    private void PrimaryInputChanged(object sender, TextChangedEventArgs e)
    {
      if (lbItems == null) return;

      var text = ((TextBox) sender).Text;
      var p = new FileAndFolderProvider();

      lbItems.Items.Clear();

      foreach (var d in p.GetElements(text))
      {
        lbItems.Items.Add(d.Text);
      }

      if (lbItems.Items.Count > 0)
      {
        lbItems.Visibility = Visibility.Visible;
        lbItems.SelectedIndex = 0;
      }
      else
      {
        lbItems.Visibility = Visibility.Hidden;
      }

    }

    private void PrimaryInputKeyPress(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Escape)
      {
        Hide();
      } 
      else if (e.Key == Key.Return)
      {
        Hide();
        string path = (string) lbItems.SelectedValue;
        Process.Start(path);

        tbInput.Text = string.Empty;
      }
    }

    private void PrimaryInputKeyPressOnGrid(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Down)
      {
        if (lbItems.SelectedIndex + 1 < lbItems.Items.Count)
          lbItems.SelectedIndex++;
      }
      else if (e.Key == Key.Up)
      {
        if (lbItems.SelectedIndex > 0 && lbItems.Items.Count > 1)
          lbItems.SelectedIndex--;
      }
    }

    private void OnWindowDeactivated(object sender, EventArgs e)
    {
      Hide();
    }
  }
}
