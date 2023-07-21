using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObjectsManagement.Application.Interfaces;
using ObjectsManagement.Domain.Entities;
using ObjectsManagementAPP.Models;

namespace ObjectsManagementAPP.Controllers
{
    public class ObjectMainsController : Controller
    {
        private readonly IObjectService _objectService;

        public IMapper _mapper { get; }
        private readonly ILogger<ObjectMainsController> _logger;

        public ObjectMainsController(IObjectService objectService, IMapper mapper, ILogger<ObjectMainsController> logger)
        {
            _objectService = objectService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: ObjectMains
        public async Task<IActionResult> Index()
        {
            try
            {
                var objectsList = await _objectService.GetAllObjects();
                if (objectsList != null)
                {
                    List<ObjectMainModel> objectMainModelList = _mapper.Map<List<ObjectMainModel>>(objectsList);
                    return View(objectMainModelList);
                }
                return Problem("Entity set 'ObjectsContext.ObjectsMain'  is null.");
            }
            catch (Exception ex)
            {
                _logger.LogError("ObjectMainsController - Index - Error: {0} - StackTrace {1}", ex.Message, ex.StackTrace);
                return Problem("Error retrieving objects");
            }
        }

        // GET: ObjectMains/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var objectMain = await _objectService.GetObjectById(id);

                if (objectMain == null)
                {
                    return NotFound();
                }

                ObjectMainModel objectMainModel = _mapper.Map<ObjectMainModel>(objectMain);
                return View(objectMainModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("ObjectMainsController - Details - Error: {0} - StackTrace {1}", ex.Message, ex.StackTrace);
                return Problem("Error retrieving details");
            }            
        }

        #region CREATE methods

        // GET: ObjectMains/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: ObjectMains/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Type")] ObjectMainModel objectMainModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ObjectMainEntity objectMain = _mapper.Map<ObjectMainEntity>(objectMainModel);
                    _objectService.ObjectCreate(objectMain);
                    await _objectService.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(objectMainModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("ObjectMainsController - Create - Error: {0} - StackTrace {1}", ex.Message, ex.StackTrace);
                return Problem("Error creating object");
            }
            
        }

        #endregion CREATE methods

        #region EDIT methods

        // GET: ObjectMains/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var objectMain = await _objectService.GetObjectById(id);

                if (objectMain == null)
                {
                    return NotFound();
                }
                ObjectMainModel objectMainModel = _mapper.Map<ObjectMainModel>(objectMain);
                return View(objectMainModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("ObjectMainsController - Edit - Error: {0} - StackTrace {1}", ex.Message, ex.StackTrace);
                return Problem("Error editing object");
            }
        }

        // POST: ObjectMains/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Type")] ObjectMainModel objectMainModel)
        {
            if (id != objectMainModel.Id)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        ObjectMainEntity objectMain = _mapper.Map<ObjectMainEntity>(objectMainModel);
                        _objectService.ObjectUpdate(objectMain);
                        await _objectService.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ObjectMainExists(objectMainModel.Id))
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
                return View(objectMainModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("ObjectMainsController - Edit - Error: {0} - StackTrace {1}", ex.Message, ex.StackTrace);
                return Problem("Error editing object");
            }            
        }

        #endregion EDIT methods

        #region DELETE methods

        // GET: ObjectMains/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var objectMain = await _objectService.GetObjectById(id);
                if (objectMain == null)
                {
                    return NotFound();
                }
                ObjectMainModel objectMainModel = _mapper.Map<ObjectMainModel>(objectMain);
                return View(objectMainModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("ObjectMainsController - Delete - Error: {0} - StackTrace {1}", ex.Message, ex.StackTrace);
                return Problem("Error deleting object");
            }            
        }


        // POST: ObjectMains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var objectMain = await _objectService.GetObjectById(id);
                if (objectMain != null)
                {
                    _objectService.ObjectRemove(objectMain);
                    await _objectService.SaveChangesAsync();
                }
                else
                {
                    return Problem("Entity set 'ObjectsContext.ObjectsMain'  is null.");
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError("ObjectMainsController - Delete - Error: {0} - StackTrace {1}", ex.Message, ex.StackTrace);
                return Problem("Error deleting object");
            }           
        }

        #endregion DELETE methods

        private bool ObjectMainExists(int id)
        {
            return _objectService.ObjectMainExists(id);
        }
    }
}
