using System;
using lilium.src.Cards;
using lilium.src.Interfaces;
using Terminal.Gui;
namespace lilium
{
    public class Program : IControlATM
    {   
        #region main
        public static void Main(string[] args)
        {
            var mainWindow = new Program();
            mainWindow.MainUserInterface();
        }
        #endregion

        #region methods
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
        
        #endregion
    }
}