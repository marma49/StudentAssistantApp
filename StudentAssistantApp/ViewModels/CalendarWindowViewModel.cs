using Caliburn.Micro;
using StudentAssistantApp.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace StudentAssistantApp.ViewModels
{
    class CalendarWindowViewModel : Screen
    {
        BindableCollection<EventModel> events = new BindableCollection<EventModel>();
        public BindableCollection<EventModel> Events { get => events; private set => events = value; }
        private DateTime selectedDate;
        private DateTime selectedTime;
        private DateTime fullDate;
        private bool isDialogOpen = false;
        private bool isNewEventDialogOpen = false;
        private string eventName;
        private string eventExplanation;
        private int eventOrderNum = 0, eventIndex = 0;


        public bool IsDialogOpen
        {
            get { return isDialogOpen; }
            set
            {
                isDialogOpen = value;
                NotifyOfPropertyChange("IsDialogOpen");
            }
        }
        public bool IsNewEventDialogOpen
        {
            get { return isNewEventDialogOpen; }
            set
            {
                isNewEventDialogOpen = value;
                NotifyOfPropertyChange("IsNewEventDialogOpen");
            }
        }
        public string EventName
        {
            get { return eventName; }
            set
            {
                eventName = value;
                NotifyOfPropertyChange("EventName");
            }
        }

        public string EventExplanation
        {
            get { return eventExplanation; }
            set
            {
                eventExplanation = value;
                NotifyOfPropertyChange("EventExplanation");
            }
        }

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set { selectedDate = value; NotifyOfPropertyChange("SelectedDate"); }
        }

        public DateTime SelectedTime
        {
            get { return selectedTime; }
            set
            {
                selectedTime = value;
                NotifyOfPropertyChange("SelectedTime");
            }
        }

        public CalendarWindowViewModel()
        {
        }

        public void AddEvent()
        {
            fullDate = new DateTime(SelectedDate.Year,
                                    SelectedDate.Month,
                                    SelectedDate.Day,
                                    SelectedTime.Hour,
                                    SelectedTime.Minute,
                                    SelectedTime.Second);

            Events.Add(new EventModel
            {
                EventName = EventName,
                EventExplanation = EventExplanation,
                EventDate = fullDate.ToString("MM/dd/yyyy hh:mm tt"),
                EventId = eventOrderNum++
            });
            IsNewEventDialogOpen = false;
        }

        public void OpenNewEvenDialog()
        {
            IsNewEventDialogOpen = true;
            EventName = "";
            EventExplanation = "";
        }

        public void CloseNewEvenDialog()
        {
            IsNewEventDialogOpen = false;
        }

        public void ShowFullEvent(string tag)
        {
            int eventId = int.Parse(tag);
            EventModel eventModel = Events.Where(n => n.EventId == eventId).First();

            IsDialogOpen = true;
            EventName = eventModel.EventName;
            EventExplanation = eventModel.EventExplanation;
        }

        public void HideFullEvent()
        {
            IsDialogOpen = false;
        }

    }
}
