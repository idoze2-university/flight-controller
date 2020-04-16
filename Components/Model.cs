using System.Windows;
namespace FlightSimulatorApp.Components
{
    class Model
    {
        public Thickness Knob_Move(object data)
        {
            Point p = (Point) data;
            double x = (p.X - ViewModel.PAD_RADIUS) / ViewModel.PAD_RADIUS;
            double y = -(p.Y - ViewModel.PAD_RADIUS) / ViewModel.PAD_RADIUS;
            Thickness t = new Thickness();
            t.Left = x;
            t.Top = y;
            return t;
        }
        public Thickness Knob_Release()
        {
            Thickness t = new Thickness();
            t.Left = 0;
            t.Top = 0;
            return t;
        }
    }
}
