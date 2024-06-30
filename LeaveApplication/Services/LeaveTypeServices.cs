using AutoMapper;
using LeaveApplication.Data;
using LeaveApplication.Models.LeaveTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaveApplication.Services
{
    public class LeaveTypeServices(ApplicationDbContext _context, IMapper _mapper) : ILeaveTypeServices
    {
      

        public async Task<List<LeaveTypeReadOnlyVM>> getAllLeaveTypes()
        {
            {
                var data = await _context.LeaveTypes.ToListAsync();
                // convert the datamodel into a view model

                var viewData = _mapper.Map<List<LeaveTypeReadOnlyVM>>(data);
                return viewData;
            }
        }

        public async Task<T?> getLeaveType<T>(int id) where T : class
        {
            var data = await _context.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
            {
                return null;
            }

            var viewData = _mapper.Map<T>(data);
            return viewData;
        }

        public async Task deleteLeaveType(int id)
        {
            var data = await _context.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                _context.Remove(data);
                await _context.SaveChangesAsync();
            }

        }

        public async Task editLeaveType(LeaveTypeEditVM model)
        {
            var leaveType = _mapper.Map<LeaveType>(model);
            _context.Update(leaveType);
            await _context.SaveChangesAsync();

        }
        public async Task createLeaveType(LeaveTypeCreateOnlyVM model)
        {
            var leaveType = _mapper.Map<LeaveType>(model);
            _context.Add(leaveType);
            await _context.SaveChangesAsync();
        }

        public bool LeaveTypeExists(int id)
        {
            return _context.LeaveTypes.Any(e => e.Id == id);
        }

        public async Task<bool> checkIfLeaveTypeNameExists(string name)
        {
            var lowerCaseName = name.ToLower();
            return await _context.LeaveTypes.AnyAsync(q => q.Name.ToLower().Equals(name));
        }

        public async Task<bool> checkIfLeaveTypeNameExistsForEdit(LeaveTypeEditVM leaveTypeEdit)
        {
            var lowerCaseName = leaveTypeEdit.Name.ToLower();
            return await _context.LeaveTypes.AnyAsync(q => q.Name.ToLower().Equals(lowerCaseName) && q.Id != leaveTypeEdit.Id);
        }

    }
}
