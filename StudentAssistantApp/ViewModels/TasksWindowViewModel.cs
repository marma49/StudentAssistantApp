using Caliburn.Micro;
using StudentAssistantApp.Models;
using System;
using System.Collections.Generic;
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

        public TasksWindowViewModel()
        {
            Tasks.Add(new TaskModel { TaskName = "Skończyć projekt", TaskExplanation="Skończyć zadanie domowe z programowania", TaskID = tasks.Count}); //TODO Сделать ограничение на длинну названия задания
            Tasks.Add(new TaskModel { TaskName = "Styudia KURWA", TaskExplanation="Napić się piwka!", TaskID = tasks.Count}); 
            Tasks.Add(new TaskModel { TaskName = "Impreza", TaskExplanation="Napić się piwka!", TaskID = tasks.Count }); 
        }

        public void AddNewTask()
        {
            tasks.Add(new TaskModel { TaskName = taskName, TaskExplanation = taskExplanation, TaskID = tasks.Count });
            TaskExplanation = "";
            TaskName = "";
        }

        public void DeleteTask(object sender)
        {
            var listBoxItem = sender as System.Windows.Controls.ListBoxItem;
            int itemIndex = int.Parse(listBoxItem.Tag.ToString());
            

            Task.Run(() =>
            {
                Thread.Sleep(300);
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    Tasks.Remove(Tasks.Where(n => n.TaskID == itemIndex).First());
                });
            });

        }
    }
}
