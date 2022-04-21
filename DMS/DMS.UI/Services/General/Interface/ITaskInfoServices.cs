using DMS.DAL.DatabaseContext;
using DMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DMS.Services.General.Interface
{
    public interface ITaskInfoServices
    {
        Task<List<Task_Info_VM>> GetAllTaskInfoVM();
        Task<List<Task_Info>> GetAllTaskInfo();
        Task<List<Task_Info_VM>> GetAllValidTaskInfoVM();
        void AddTaskInfo(Task_Info_VM Task_Info_VM);
        void DeleteTaskInfo(int id);
        void EditTaskInfo(Task_Info_VM Task_Info_VM);
        Task<Task_Info_VM> GetTaskInfobyId(int id);
        List<Task_Info> GetAllTaskInfobyEmployeeId(int id);
        Task<List<Task_Info_VM>> GetAllAssignedTask();
        Task<List<Task_Info_VM>> GetAllCreatedTask();
        List<Task_Info_VM> GetAllAssignedTaskSync();
        void EditAssignedTask(Task_Info_VM ValidData);

    }
}
