using Caliburn.Micro;
using StudentAssistantApp.Models;
using StudentAssistantApp.ModelsDB;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        EventModel eventModel; 
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
            //Pobranie i wyswietlenie danych z bazy danych
            using(var context = new StudentAppContext())
            {
                var events = context.DBEvents.ToList();
                foreach(DBEvent e1 in events)
                {
                    Events.Add(new EventModel 
                    { 
                        EventName = e1.EventTitle,
                        EventExplanation = e1.EventContent,
                        EventDate = e1.DateEvent.ToString("MM/dd/yyyy hh:mm tt"),
                        EventId = e1.DBEventId
                    });
                }
            }
        }

        public void AddEvent()
        {
            fullDate = new DateTime(SelectedDate.Year,
                                    SelectedDate.Month,
                                    SelectedDate.Day,
                                    SelectedTime.Hour,
                                    SelectedTime.Minute,
                                    SelectedTime.Second);
         

            //Dodanie do bazy danych
            int orderId;
            var dbevent = new DBEvent { EventTitle = eventName, EventContent = eventExplanation, DateEvent = fullDate };
            using(StudentAppContext context = new StudentAppContext())
            {
                context.DBEvents.Add(dbevent);
                context.SaveChanges();
                var ostatni = context.DBEvents.OrderByDescending(x => x.DBEventId).FirstOrDefault();
                orderId = ostatni.DBEventId;
            }

            Events.Add(new EventModel
            {
                EventName = EventName,
                EventExplanation = EventExplanation,
                EventDate = fullDate.ToString("MM/dd/yyyy hh:mm tt"),
                EventId = orderId
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
            eventModel = Events.Where(n => n.EventId == eventId).First();

            IsDialogOpen = true;
            EventName = eventModel.EventName;
            EventExplanation = eventModel.EventExplanation;
        }

        public async void DeleteEvent()
        {
            IsDialogOpen = false;
            await Task.Delay(200);
            Events.Remove(eventModel);
        }

        public void HideFullEvent()
        {
            IsDialogOpen = false;
        }

    }
}
