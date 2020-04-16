using System;
using System.Windows.Controls;
using FlightSimulatorApp.Components;
namespace FlightSimulatorApp.Controllers
{
    public partial class Throttle : UserControl
    {
        ViewModel vm = ViewModel.getInstance();
        public delegate void View();
        public Throttle()
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
