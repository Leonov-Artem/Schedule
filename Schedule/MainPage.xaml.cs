using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Schedule.ScheduleType;

namespace Schedule
{
    public partial class MainPage : ContentPage
    {
        const string NOTIFICATION = "Уведомление";
        const string ERROR = "Ошибка!";
        const string CANCEL_OK = "OK";
        const int FONT_SIZE = 24;
        const double SCALE = 1.5;

        Label _currentScheduleTypeLabel;
        Label _datePickerLabel;
        Button _alertButton;
        Picker _scheduleTypePicker;
        DatePicker _datePicker;
        DateTime _newDate = DateTime.Now;
        Schedule _schedule;
        string _choice;

        public MainPage()
        {
            CreateScheduleTypeLabel();
            CreateScheduleTypePicker();
            CreateDatepickerLabel();
            CreateDatePicker();
            CreateAlertButton();
            DisplayCreatedItems();
        }

        #region event handlers

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            _choice = _scheduleTypePicker.Items[SelectedIndex()];
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            _newDate = e.NewDate;
        }

        private void AlertButton_Clicked(object sender, EventArgs e)
        {
            if (SelectedIndex() != -1 && _newDate.Date != DateTime.Now.Date)
            {
                string res = CalculateResult();
                DisplayResult(res);
            }
            else
                DisplayAlert(ERROR, "Выберите все элементы!", CANCEL_OK);
        }

        #endregion

        private int SelectedIndex()
            => _scheduleTypePicker.SelectedIndex;

        private string CalculateResult()
        {
            _schedule = new Schedule();
            return _schedule.Calculate(_choice, _newDate);
        }

        private void DisplayResult(string result)
        {
            if (result == DAY || result == NIGHT)
            {
                DisplayAlert
                (
                        NOTIFICATION,
                        $"{DateToString()} вы " +
                        $"{result}",
                        CANCEL_OK
                 );
            }
            else if (result == AFTER_NIGHT || result == DAY_OFF)
            {
                DisplayAlert
                (
                        NOTIFICATION,
                        $"{DateToString()} у вас " +
                        $"{result}",
                        CANCEL_OK
                 );
            }
        }

        private string DateToString()
            => _newDate.ToString("dd/MM/yyyy");

        #region displayed items

        private void CreateScheduleTypeLabel()
        {
            _currentScheduleTypeLabel = new Label
            {
                Text = "Сегодня:",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
        }

        private void CreateScheduleTypePicker()
        {
            _scheduleTypePicker = new Picker 
            { 
                Title = "Выбрать вариант",  
                FontSize = FONT_SIZE
            };

            _scheduleTypePicker.Items.Add(DAY);
            _scheduleTypePicker.Items.Add(NIGHT);
            _scheduleTypePicker.Items.Add(AFTER_NIGHT);
            _scheduleTypePicker.Items.Add(DAY_OFF);
            _scheduleTypePicker.SelectedIndexChanged += Picker_SelectedIndexChanged;
        }

        private void CreateDatepickerLabel()
        {
            _datePickerLabel = new Label
            {
                Text = "Выберите дату:",
                FontSize = FONT_SIZE
            };
        }

        private void CreateDatePicker()
        {
            _datePicker = new DatePicker 
            {
                Format = "D",
                FontSize = FONT_SIZE
            };
            _datePicker.DateSelected += DatePicker_DateSelected;
        }

        private void CreateAlertButton()
        {
            _alertButton = new Button
            {
                Text = "Посчитать",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Scale = SCALE
            };
            _alertButton.Clicked += AlertButton_Clicked;
        }

        private void DisplayCreatedItems()
        {
            var stack = new StackLayout
            {
                Children =
                {
                    _currentScheduleTypeLabel,
                    _scheduleTypePicker,
                    new Label { Text = "\n\n\n" },
                    _datePickerLabel,
                    _datePicker,
                    _alertButton
                }
            };
            this.Content = stack;
        }

        #endregion
    }
}

