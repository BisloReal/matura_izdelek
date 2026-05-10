using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace evidenca_krav
{
    public interface IRefreshable
    {
        void Posodobi();
    }

    public class NavigationNadzor
    {
        List<UserControl> userControlList = new List<UserControl>();
        Panel panel;

        public NavigationNadzor(List<UserControl> userConList, Panel pan)
        {
            userControlList = userConList;
            panel = pan;
            DodajUserControls();
        }

        private void DodajUserControls()
        {
            foreach (var userControl in userControlList)
            {
                panel.Controls.Add(userControl);
                userControl.Dock = DockStyle.Fill;
            }
        }

        public void Prikaz(int index)
        {
            if (index < userControlList.Count)
            {
                UserControl control = userControlList[index];

                control.BringToFront();

                if (control is IRefreshable refreshable)
                {
                    refreshable.Posodobi();
                }
            }
        }
    }
}
