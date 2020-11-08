using Caliburn.Micro;
using StudentAssistantApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
            Tasks.Add(new TaskModel { TaskName = "Skończyć projekt", TaskExplanation="Skończyć zadanie domowe z programowania"}); //TODO Сделать ограничение на длинну названия задания
            Tasks.Add(new TaskModel { TaskName = "Impreza", TaskExplanation="Napić się piwka!"}); 
            Tasks.Add(new TaskModel { TaskName = "Impreza", TaskExplanation="Napić się piwka!"}); 
        }

        public void AddNewTask()
        {
            tasks.Add(new TaskModel { TaskName = taskName, TaskExplanation = taskExplanation });
            TaskExplanation = "";
            TaskName = "";
        }
    }
}
