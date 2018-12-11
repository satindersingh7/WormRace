using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WormRaceInWPF
{
    /// <summary>
    /// Interaction logic for Worm.xaml
    /// </summary>
    public partial class Worm : UserControl
    {
        public static readonly DependencyProperty WormColorProperty = DependencyProperty.Register
                                                ("WormColor", typeof(Brush), typeof(Worm),
                                                  new FrameworkPropertyMetadata(new PropertyChangedCallback(OnWormColorChanged)));


        public Brush WormColor
        {
            get { return (Brush)GetValue(WormColorProperty); }
            set { SetValue(WormColorProperty, value); }
        }

        public Worm()
        {
            InitializeComponent();
          
        }

        private static void OnWormColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Worm wrm = (Worm)sender;
            Brush b = wrm.WormColor;
            wrm.ellipse.Fill = b;
            wrm.ellipse1.Fill = b;            
            wrm.ellipse3.Fill = b;
            wrm.ellipse4.Fill = b;
                       
        }
    }
}
