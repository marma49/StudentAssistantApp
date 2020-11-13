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
        private int itemIndex, itemCount = 0;

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
            Tasks.Add(new TaskModel { TaskName = "Skończyć projekt", TaskExplanation="Skończyć zadanie domowe z programowania", TaskID = itemCount++}); //TODO Сделать ограничение на длинну названия задания
            Tasks.Add(new TaskModel { TaskName = "Styudia", TaskExplanation="Zrobić zadanie z programowania!", TaskID = itemCount++ }); 
            Tasks.Add(new TaskModel { TaskName = "Impreza", TaskExplanation="Napić się piwka!", TaskID = itemCount++});
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
            }
            else
            {
                tasks.Add(new TaskModel { TaskName = taskName, TaskExplanation = taskExplanation, TaskID = itemCount++});
            }

            TaskExplanation = "";
            TaskName = "";
            IsDialogOpen = false;
            isEditing = false;
        }
        public void DeleteTask(object sender)
        {
            var listBoxItem = sender as System.Windows.Controls.ListBoxItem; //Get sender
            itemIndex = int.Parse(listBoxItem.Tag.ToString()); //Convert  listBoxItem.Tag to int (TaskID)
            TaskModel task = Tasks.Where(n => n.TaskID == itemIndex).First();


            Task.Run(() =>
            {
                Thread.Sleep(300);//A short delay to make more smooth animation of removing
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    Tasks.Remove(task); //Get and remove selected task
                });
            });

            if (Tasks.Count == 0)
            {
                itemCount = 0;
            }
        }
        public void EditTask(object sender)
        {
            var listBoxItem = sender as System.Windows.Controls.ListBoxItem; //Get sender
            itemIndex = int.Parse(listBoxItem.Tag.ToString()); //Convert  listBoxItem.Tag to int (TaskID)
            TaskModel task = Tasks.Where(n => n.TaskID == itemIndex).First();
            itemIndex = Tasks.IndexOf(task);
            
            IsDialogOpen = true;
            isEditing = true;

            TaskName = Tasks[itemIndex].TaskName;
            TaskExplanation = Tasks[itemIndex].TaskExplanation;
        }

     
    }
}
