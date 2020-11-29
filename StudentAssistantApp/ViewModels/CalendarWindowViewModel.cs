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

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set { selectedDate = value; NotifyOfPropertyChange("SelectedDate"); }
        }

        public CalendarWindowViewModel()
        {
            SelectedDate = DateTime.Now;
            Events.Add(new EventModel { EventName = "The First Event Name!", EventDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")});
            Events.Add(new EventModel { EventName = "The First Event Name!", EventDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")});
            Events.Add(new EventModel { EventName = "The First Event Name!", EventDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")});
            Events.Add(new EventModel { EventName = "The First Event Name!", EventDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")});
        }

        public void AddEvent()
        {
            Events.Add(new EventModel { EventName = "The First Event Name!", EventDate = SelectedDate.ToString("MM/dd/yyyy hh:mm tt")});
        }

    }
}
