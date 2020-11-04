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

        public BindableCollection<TaskModel> Tasks
        {
            get { return tasks; }
            set { tasks = value; }
        }

        public TasksWindowViewModel()
        {
            Tasks.Add(new TaskModel { TaskName = "Skończyć projekt z programowania", TaskExplanation="Skończyć zadanie domowe z programowania"}); 
            Tasks.Add(new TaskModel { TaskName = "Impreza", TaskExplanation="Napić się piwka!"}); 
            Tasks.Add(new TaskModel { TaskName = "Impreza", TaskExplanation="Napić się piwka!"}); 
        }

        public void AddNewTask()
        {
            tasks.Add(new TaskModel { TaskName = "Nowe dziwne zadanie", TaskExplanation="Jakaś dziwna definicja, dziwnego zadania! xD"});
        }
    }
}
