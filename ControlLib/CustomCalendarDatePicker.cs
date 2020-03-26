using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace Infonet.CStoreCommander.ControlLib
{
    public sealed class CustomCalendarDatePicker : CalendarDatePicker
    {
        public DateTimeOffset MinDateOffSet
        {
            get { return (DateTimeOffset)GetValue(MinDateOffSetProperty); }
            set { SetValue(MinDateOffSetProperty, value); }
        }

        public static readonly DependencyProperty MinDateOffSetProperty =
            DependencyProperty.Register(nameof(MinDateOffSet), typeof(DateTimeOffset), typeof(CustomCalendarDatePicker), new PropertyMetadata(DateTimeOffset.MinValue));


        public DateTimeOffset SelectedDate
        {
            get { return (DateTimeOffset)GetValue(SelectedDateProperty); }
            set { SetValue(SelectedDateProperty, value); }
        }

        public static readonly DependencyProperty SelectedDateProperty =
            DependencyProperty.Register(nameof(SelectedDate),
                typeof(DateTimeOffset),
                typeof(CustomCalendarDatePicker),
                new PropertyMetadata(null, (sender, e) =>
                {
                    if (e.NewValue != null)
                    {
                        ((CustomCalendarDatePicker)sender).Date = (DateTimeOffset)e.NewValue;
                    }
                    else
                    {
                        ((CustomCalendarDatePicker)sender).Date = null;
                    }
                }));

        public CustomCalendarDatePicker()
        {
            this.DefaultStyleKey = typeof(CustomCalendarDatePicker);
        }


        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.DateChanged -= CustomCalendarDatePickerDateChanged;
            this.DateChanged += CustomCalendarDatePickerDateChanged;
        }

        private void CustomCalendarDatePickerDateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (args.NewDate != args.OldDate)
            {
                if (args.NewDate != null && args.NewDate.HasValue)
                {
                    SelectedDate = args.NewDate.Value;
                }
                else if (args.OldDate != null && args.OldDate.HasValue)
                {
                    SelectedDate = args.OldDate.Value;
                    Date = args.OldDate;
                }
                else
                {
                    SelectedDate = DateTimeOffset.Now;
                }
            }
        }
    }
}
