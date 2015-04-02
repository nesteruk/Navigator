using System.Windows.Forms;
using Navigator;

namespace WinJump
{
  class JumpContextMenu : ContextMenu
  {
    private MenuItem quitItem;

    public JumpContextMenu(Jumper jumper)
    {
      quitItem = new MenuItem("Quit");
      quitItem.Click += (sender, args) => Application.Exit();
      MenuItems.Add(quitItem);
    }
  }
}