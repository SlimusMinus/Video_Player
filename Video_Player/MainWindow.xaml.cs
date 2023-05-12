using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Video_Player
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Image img = new Image();
        Image img2 = new Image();
        DispatcherTimer timer = new DispatcherTimer();
        TextBlock textBlock_play;
        TextBlock textBlock_pause;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            slider_play.Value = Window_Player.Position.TotalSeconds;
            if (Window_Player.Position.Minutes < 10 && Window_Player.Position.Seconds < 10)
                label_time.Content = 0 + Convert.ToInt32(Window_Player.Position.Minutes).ToString() + ":" + 0 + Convert.ToInt32(Window_Player.Position.Seconds).ToString();
            else if (Window_Player.Position.Minutes < 10 && Window_Player.Position.Seconds >= 10)
                label_time.Content = 0 + Convert.ToInt32(Window_Player.Position.Minutes).ToString() + ":" + Convert.ToInt32(Window_Player.Position.Seconds).ToString();
            else
                label_time.Content = Convert.ToInt32(Window_Player.Position.Minutes).ToString() + ":" + Convert.ToInt32(Window_Player.Position.Seconds).ToString();
        }

        private void BT_Stop(object sender, RoutedEventArgs e)
        {
            Window_Player.Stop();
            bt_Play.Content = img;
        }

        private void BT_Clouse(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            if (bt_Play.Content == img)
            {
                Window_Player.Play();
                bt_Play.Content = img2;
                bt_Play.ToolTip = textBlock_pause;
            }
            else if (bt_Play.Content == img2)
            {
                Window_Player.Pause();
                bt_Play.Content = img;
                bt_Play.ToolTip = textBlock_play;
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Window_Player.Volume = slider_vol.Value / 100;
        }

        private void Window_Player_MediaOpened(object sender, RoutedEventArgs e)
        {
            slider_play.Maximum = Window_Player.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void Window_Player_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show("Incorrect format media file", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            img.Source = new BitmapImage(new Uri("play.ico", UriKind.Relative));
            img2.Source = new BitmapImage(new Uri("pause.ico", UriKind.Relative));
            bt_Play.Content = img;
            Window_Player.Volume = slider_vol.Value;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            textBlock_play = new TextBlock();
            textBlock_pause = new TextBlock();
            textBlock_play.Text = "Play";
            textBlock_pause.Text = "Pause";
            bt_Play.ToolTip = textBlock_play;

           
        }

        private void slider_play_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Window_Player.Position = TimeSpan.FromSeconds(slider_play.Value);
        }

        private void BT_Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
                Window_Player.Source = new Uri(openFile.FileName);
            Window_Player.Play();
            bt_Play.Content = img2;
        }

      
    }
}
