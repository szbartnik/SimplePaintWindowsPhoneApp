using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DrawOnMe
{
    public class MasterViewModel : INotifyPropertyChanged
    {
        private int _thickness = 10;
        public int Thickness {
            get { return _thickness; }
            set
            {
                if (value != _thickness && value > 0 && value < 100)
                {
                    _thickness = value;
                    NotifyPropertyChanged("Thickness");
                }
            }
        }

        private SolidColorBrush _bgColor = new SolidColorBrush(Colors.White);
        public SolidColorBrush BgColor
        {
            get { return _bgColor; }
            set
            {
                if (value != _bgColor)
                {
                    _bgColor = value;
                    NotifyPropertyChanged("BgColor");
                }
            }
        }

        private SolidColorBrush _lineColor = new SolidColorBrush(Colors.Black);
        public SolidColorBrush LineColor
        {
            get { return _lineColor; }
            set
            {
                if (value != _lineColor)
                {
                    _lineColor = value;
                    NotifyPropertyChanged("LineColor");
                }
            }
        }

        #region Events Stuff

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
