using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using WinJump;

namespace Navigator
{
  public class Jumper : IDisposable
  {
    private static readonly Lazy<Jumper> instance = new Lazy<Jumper>(() => new Jumper());

    private Hotkey hotkey;
    private Control ctl;
    private JumpMenu jumpMenu;

    private Jumper()
    {
      CreateNotifyIcon();

      jumpMenu = new JumpMenu();
      ElementHost.EnableModelessKeyboardInterop(jumpMenu);

      hotkey = new Hotkey(Keys.N, false, false, false, true);
      ctl = new Control();
      //bool canRegister = hotkey.GetCanRegister(ctl);
      hotkey.Register(ctl);
      hotkey.Pressed += HotkeyOnPressed;
    }

    /// <summary>
    /// Manufactures the notification icon that lives for a very long time.
    /// </summary>
    private void CreateNotifyIcon()
    {
      var notify = new NotifyIcon();
      notify.Text = "WinJump";
      notify.Icon = new Form().Icon;
      notify.ContextMenu = new JumpContextMenu(this);
      notify.DoubleClick += Configure;
      notify.Visible = true;
    }

    private void HotkeyOnPressed(object sender, HandledEventArgs handledEventArgs)
    {
      jumpMenu.Visibility = jumpMenu.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
      if (jumpMenu.Visibility == Visibility.Visible)
      {
        jumpMenu.Activate();
        jumpMenu.FocusInput();
      }
    }

    private void Configure(object sender, EventArgs eventArgs)
    {
      
    }

    public static Jumper Instance
    {
      get {
        return instance.Value;
      }
    }

    public void Dispose()
    {
      hotkey.Unregister();
      ctl.Dispose();
    }
  }
}