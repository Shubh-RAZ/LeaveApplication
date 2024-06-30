using LeaveApplication.Models.LeaveTypes;

namespace LeaveApplication.Services
{
    public interface ILeaveTypeServices
    {
        Task<bool> checkIfLeaveTypeNameExists(string name);
        Task<bool> checkIfLeaveTypeNameExistsForEdit(LeaveTypeEditVM leaveTypeEdit);
        Task createLeaveType(LeaveTypeCreateOnlyVM model);
        Task deleteLeaveType(int id);
        Task editLeaveType(LeaveTypeEditVM model);
        Task<List<LeaveTypeReadOnlyVM>> getAllLeaveTypes();
        Task<T?> getLeaveType<T>(int id) where T : class;
        bool LeaveTypeExists(int id);
    }
}