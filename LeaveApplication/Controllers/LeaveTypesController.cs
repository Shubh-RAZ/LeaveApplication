using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveApplication.Data;
using LeaveApplication.Models.LeaveTypes;
using AutoMapper;
using LeaveApplication.Services;
using AspNetCore;

namespace LeaveApplication.Controllers
{
    public class LeaveTypesController(ILeaveTypeServices _leaveTypeServices) : Controller
    {
   
        private static string NameExistValidationMessage = "This leaveType already exist in database";



        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {
            var viewData = _leaveTypeServices.getAllLeaveTypes();
            return View(viewData);
        }

        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _leaveTypeServices.getLeaveType<LeaveTypeReadOnlyVM>(id.Value);
            if (leaveType == null)
            {
                return NotFound();
            }


            return View(leaveType);
        }

        // GET: LeaveTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (LeaveTypeCreateOnlyVM leaveTypeCrete)
        {

            if (leaveTypeCrete.Name.Contains("vacation")){
                ModelState.AddModelError(nameof(leaveTypeCrete.Name), "Name should not contain vacation");
            }

            if (await _leaveTypeServices.checkIfLeaveTypeNameExists(leaveTypeCrete.Name))
            {
                ModelState.AddModelError(nameof(leaveTypeCrete.Name), " This Leave Type already exist in the database");
            }

            if (ModelState.IsValid)
            {
                var leaveType = _leaveTypeServices.createLeaveType(leaveTypeCrete);
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeCrete);
        }

     
        // GET: LeaveTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _leaveTypeServices.getLeaveType<LeaveTypeEditVM>(id.Value);
            if (leaveType == null)
            {
                return NotFound();
            }

         
            return View(leaveType);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveTypeEditVM leaveTypeEdit)
        {
            if (id != leaveTypeEdit.Id)
            {
                return NotFound();
            }

            if (await _leaveTypeServices.checkIfLeaveTypeNameExistsForEdit(leaveTypeEdit))
            {
                ModelState.AddModelError(nameof(leaveTypeEdit.Name), NameExistValidationMessage);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var leaveType = _leaveTypeServices.editLeaveType(leaveTypeEdit);


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_leaveTypeServices.LeaveTypeExists(leaveTypeEdit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeEdit);
        }

       

        // GET: LeaveTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _leaveTypeServices.getLeaveType<LeaveTypeReadOnlyVM>(id.Value);
            if (leaveType == null)
            {
                return NotFound();
            }

            return View(leaveType);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
            await _leaveTypeServices.deleteLeaveType(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
