using Caliburn.Micro;
using StudentAssistantApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace StudentAssistantApp.ViewModels
{
    class TasksWindowViewModel : Screen
    {
        private BindableCollection<TaskModel> tasks = new BindableCollection<TaskModel>();
        private string taskName;
        private string taskExplanation;
        private bool isDialogOpen, isEditing;
        private int itemIndex, itemCount = 1;

        public BindableCollection<TaskModel> Tasks
        {
            get { return tasks; }
            set { tasks = value; }
            
        }
        public string TaskName
        {
            get { return taskName; }
            set { taskName = value;
                NotifyOfPropertyChange("TaskName");
            }
        }

        public string TaskExplanation
        {
            get { return taskExplanation; }
            set { taskExplanation = value;
                NotifyOfPropertyChange("TaskExplanation");
            }
        }

        public bool IsDialogOpen
        {
            get { return isDialogOpen; }
            set { isDialogOpen = value;
                NotifyOfPropertyChange("IsDialogOpen");
            }
        }

        public TasksWindowViewModel()
        {
            //Tasks.Add(new TaskModel { TaskName = "Skończyć projekt", TaskExplanation="Skończyć zadanie domowe z programowania", TaskID = itemCount++}); //TODO Показывать название при наведении мышки
            //Tasks.Add(new TaskModel { TaskName = "Styudia", TaskExplanation="Zrobić zadanie z programowania!", TaskID = itemCount++ }); 
            //Tasks.Add(new TaskModel { TaskName = "Impreza", TaskExplanation="Napić się piwka!", TaskID = itemCount++});
            
            //Czytanie z bazy i wyświetlenie
           using(var context = new StudentAppContext())
            {
                var tasks = context.DBTasks.ToList();
                foreach(DBTask t1 in tasks)
                {
                    Tasks.Add(new TaskModel { TaskName = t1.TaskHeadline, TaskExplanation = t1.TaskText, TaskID = t1.DBTaskId });
                }

                //return context.DBTasks.ToList();
            }
        }

        public void CloseDialog()
        {
            IsDialogOpen = false;
            isEditing = false;
            TaskExplanation = "";
            TaskName = "";
        }
        public void AddNewTask()
        {
            if (isEditing)
            {
                Tasks[itemIndex].TaskName = TaskName;
                Tasks[itemIndex].TaskExplanation = TaskExplanation;
                Tasks.Refresh(); //Raises a change notification indicating that all bindings should be refreshed.

                //Modyfikacja w bazie
                /*
                using (var context = new StudentAppContext())
                {
                    DBTask DBTask = context.DBTasks.Where(x => x.DBTaskId == 4).FirstOrDefault(); //Do poprawy idIndex
                    DBTask.TaskHeadline = TaskName;
                    DBTask.TaskText = TaskExplanation;

                    context.SaveChanges();
                }*/
            }
            else
            {

                //Dodawanie do bazy
                var dbtask = new DBTask { TaskHeadline = taskName, TaskText = taskExplanation };
                int itemId;
                using (var context = new StudentAppContext())
                {
                    context.DBTasks.Add(dbtask);
                    context.SaveChanges();
                    var obiektChwilowy = context.DBTasks.OrderByDescending(x => x.DBTaskId).FirstOrDefault();
                    itemId = obiektChwilowy.DBTaskId;
                }

                tasks.Add(new TaskModel { TaskName = taskName, TaskExplanation = taskExplanation, TaskID = itemId });
            }

            TaskExplanation = "";
            TaskName = "";
            IsDialogOpen = false;
            isEditing = false;
        }
        public async Task DeleteTaskAsync(object sender)
        {
            var listBoxItem = sender as System.Windows.Controls.ListBoxItem; //Get sender
            itemIndex = int.Parse(listBoxItem.Tag.ToString()); //Convert  listBoxItem.Tag to int (TaskID)
            TaskModel task = Tasks.Where(n => n.TaskID == itemIndex).First();


            await Task.Delay(300);//A short delay to make more smooth animation of removing
            Tasks.Remove(task); //Get and remove selected task
            /*if (Tasks.Count == 0)
            {
                itemCount = 0;
            }*/

            //Usuniecie z bazy
            using (var context = new StudentAppContext())
            {
                DBTask DBTask = context.DBTasks.Where(x => x.DBTaskId == itemIndex).FirstOrDefault();
                context.DBTasks.Remove(DBTask);

                context.SaveChanges();
            }
        }
        public void EditTask(object sender)
        {
            var listBoxItem = sender as System.Windows.Controls.ListBoxItem; //Get sender
            itemIndex = int.Parse(listBoxItem.Tag.ToString()); //Convert  listBoxItem.Tag to int (TaskID)
            int itemindex = itemIndex;
            TaskModel task = Tasks.Where(n => n.TaskID == itemIndex).First();
            itemIndex = Tasks.IndexOf(task);

            IsDialogOpen = true;
            isEditing = true;

            TaskName = Tasks[itemIndex].TaskName;
            TaskExplanation = Tasks[itemIndex].TaskExplanation;

            //Modyfikacja w bazie
            using (var context = new StudentAppContext())
            {
                DBTask DBTask = context.DBTasks.Where(x => x.DBTaskId == itemindex).FirstOrDefault(); //Do poprawy idIndex
                DBTask.TaskHeadline = TaskName;
                DBTask.TaskText = TaskExplanation;

                context.SaveChanges();
            }
        }
    }
}
