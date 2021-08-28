using System;
using Terminal.Gui;
using lilium.src.Interfaces;
namespace lilium.src.Guis
{
    internal class GUI : IControlATM
    {
        public void MainUserInterface()
        {
            Application.Init();
            var windows = new Window("Liliu ATM Network - CTRL-Q to quit")
            {
                X = 20,
                Y = 10,
                Width = Dim.Fill(10),
                Height = Dim.Fill(10)
            };
            Application.Top.Add(windows);
            Application.Run();
        }
    }
}