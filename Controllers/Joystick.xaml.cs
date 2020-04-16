using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FlightSimulatorApp.Components;
using System;
namespace FlightSimulatorApp.Controllers
{
    public partial class Joystick : UserControl
    {
        ViewModel vm = ViewModel.getInstance();
        public Joystick()
        {
            InitializeComponent();
            DataContext = vm;
        }
        private void Knob_Move(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point p = e.GetPosition(inner_Pad);
                vm.Command(Commands.Commands.KNOB_MOVE_CMD, p);
            }
        }
        private void Knob_Release(object sender, EventArgs e)
        {
            vm.Command(Commands.Commands.KNOB_RELEASE_CMD, null);
        }
    }
}
