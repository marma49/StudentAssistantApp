using Caliburn.Micro;
using StudentAssistantApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAssistantApp.ViewModels
{
    class CalendarWindowViewModel : Screen
    {
        BindableCollection<EventModel> events = new BindableCollection<EventModel>();
        public BindableCollection<EventModel> Events { get => events; private set => events = value; }
        private DateTime selectedDate;
        private bool isDialogOpen = false;
        private bool isNewEventDialogOpen = false;
        private string eventName;
        private string eventExplanation;


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

        public CalendarWindowViewModel()
        {
            SelectedDate = DateTime.Now;
            Events.Add(new EventModel { EventName = "The First Event Name!", EventDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") });
            Events.Add(new EventModel { EventName = "The First Event Name!", EventDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") });
            Events.Add(new EventModel { EventName = "The First Event Name!", EventDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") });
            Events.Add(new EventModel { EventName = "The First Event Name!", EventDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") });
        }

        public void AddEvent()
        {
            Events.Add(new EventModel { EventName = EventName, EventExplanation = EventExplanation, EventDate = SelectedDate.ToString("MM/dd/yyyy hh:mm tt") });
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

        public void ShowFullEvent(object source)
        {
            IsDialogOpen = true;
        }

    }
}
