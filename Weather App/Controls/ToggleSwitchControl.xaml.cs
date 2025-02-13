using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Media;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.ComponentModel;


namespace Weather_App.Controls
{
    public partial class ToggleSwitchControl
    {
        #region Constructor
        public ToggleSwitchControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Fields & Dependecy Properties

        // IsChecked Property
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register("IsChecked", typeof(bool), typeof(ToggleSwitchControl),
            new PropertyMetadata(false, OnIsCheckedChanged));

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ToggleSwitchControl toggleSwitch)
            {
                toggleSwitch.UpdateVisualState((bool)e.NewValue);
            }
        }

        private void ToggleSwitch_Click(object sender, RoutedEventArgs e)
        {
            IsChecked = !IsChecked; // Toggle the state
        }

        private void UpdateVisualState(bool isChecked)
        {
            if (ThumbTransform == null || Track == null) return;

            double trackWidth = Track.ActualWidth;
            double trackHeight = Track.ActualHeight;
            double thumbWidth = Thumb.ActualWidth;
            double maxMove = trackWidth - thumbWidth - 4;  // Adjust based on padding

            double toValue = isChecked ? maxMove : 2; // Move Right (ON) or Left (OFF)

            // Ensure the background is not frozen
            if (Track.Background is SolidColorBrush frozenBrush && frozenBrush.IsFrozen)
            {
                Track.Background = new SolidColorBrush(((SolidColorBrush)Track.Background).Color);
            }

            if (Track.Background is SolidColorBrush backgroundBrush)
            {
                backgroundBrush.BeginAnimation(SolidColorBrush.ColorProperty, new ColorAnimation
                {
                    To = isChecked ? Colors.Green : Colors.Gray,
                    Duration = TimeSpan.FromMicroseconds(200)
                });
            }

            // Animate the Toggle Thumb
            var thumbAnimation = new DoubleAnimation
            {
                To = toValue,
                Duration = TimeSpan.FromMilliseconds(200),
                EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseInOut }
            };
            ThumbTransform.BeginAnimation(TranslateTransform.XProperty, thumbAnimation);

        }
        #endregion

    }
}
