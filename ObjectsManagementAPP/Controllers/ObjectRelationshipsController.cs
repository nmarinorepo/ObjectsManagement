using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ObjectsManagement.Application.Interfaces;
using ObjectsManagement.Domain.Entities;
using ObjectsManagementAPP.Models;

namespace ObjectsManagementAPP.Controllers
{
    public class ObjectRelationshipsController : Controller
    {
        private readonly IObjectService _objectService;
        public IMapper _mapper { get; }
        private readonly ILogger<ObjectRelationshipsController> _logger;

        public ObjectRelationshipsController(IObjectService objectService, IMapper mapper, ILogger<ObjectRelationshipsController> logger)
        {
            _objectService = objectService;
            _mapper = mapper;
            _logger = logger;
        }

        #region CREATE methods

        // GET: ObjectRelationships/Create
        public IActionResult Create(string id)
        {
            try
            {
                ObjectViewModel objectViewModel = new ObjectViewModel() { ObjectMainId = id, objectRelationship = new ObjectRelationshipModel() };
                return View(objectViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("ObjectRelationshipsController - Create - Error: {0} - StackTrace {1}", ex.Message, ex.StackTrace);
                return Problem("Error creating relationship");
            }            
        }

        // POST: ObjectRelationships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            try
            {
                var objectMain = await _objectService.GetObjectById(int.Parse(collection["ObjectMainId"]));
                if (objectMain.ObjectRelationships == null)
                {
                    objectMain.ObjectRelationships = new List<ObjectRelationshipEntity>();
                }

                if (ModelState.IsValid)
                {
                    ObjectRelationshipModel objectRelationshipModel = new ObjectRelationshipModel()
                    {
                        Name = collection["objectRelationship.Name"],
                        Description = collection["objectRelationship.Description"],
                        Type = collection["objectRelationship.Type"],
                        ObjectMainId = int.Parse(collection["ObjectMainId"])
                    };

                    var objectRelationships = _mapper.Map<ObjectRelationshipEntity>(objectRelationshipModel);
                    objectMain.ObjectRelationships.Add(objectRelationships);
                    _objectService.ObjectUpdate(objectMain);
                    await _objectService.SaveChangesAsync();

                    return RedirectToAction("Index", "ObjectMains");
                }
                return View(objectMain.ObjectRelationships);
            }
            catch (Exception ex)
            {
                _logger.LogError("ObjectRelationshipsController - Create - Error: {0} - StackTrace {1}", ex.Message, ex.StackTrace);
                return Problem("Error creating relationship");
            }            
        }

        #endregion CREATE methods

        #region EDIT methods

        // GET: ObjectRelationships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var objectMainList = await _objectService.GetAllObjects();
                var objectMainModelList = _mapper.Map<List<ObjectMainModel>>(objectMainList);

                var objectRelationshipList = await _objectService.GetAllRelationshipsWithParent();
                var objectRelationship = objectRelationshipList.FirstOrDefault(m => m.Id == id);
                if (objectRelationship == null)
                {
                    return NotFound();
                }

                var objectRelationshipModel = _mapper.Map<ObjectRelationshipModel>(objectRelationship);

                ViewBag.ObjectsMain = new SelectList(objectMainModelList, "Id", "Name", objectRelationshipModel.ObjectMainId);

                return View(objectRelationshipModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("ObjectRelationshipsController - Edit - Error: {0} - StackTrace {1}", ex.Message, ex.StackTrace);
                return Problem("Error editing relationship");
            }            
        }

        // POST: ObjectRelationships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormCollection collection)    //int id, [Bind("Id,Name,Description,Type,ObjectMainId")] ObjectRelationship objectRelationship
        {
            try
            {                
                if (ModelState.IsValid)
                {
                    try
                    {
                        ObjectRelationshipModel objectRelationshipModel = new ObjectRelationshipModel()
                        {
                            Id = int.Parse(collection["Id"]),
                            Name = collection["Name"],
                            Description = collection["Description"],
                            Type = collection["Type"],
                            ObjectMainId = int.Parse(collection["ObjectMainId"])
                        };
                       
                        var objectRelationship = _mapper.Map<ObjectRelationshipEntity>(objectRelationshipModel);
                        _objectService.ObjectRelationshipUpdate(objectRelationship);
                        await _objectService.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ObjectRelationshipExists(int.Parse(collection["Id"])))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("Edit", "ObjectMains", new { id = int.Parse(collection["ObjectMain.Id"]) });
                }
                var objectMainList = await _objectService.GetAllObjects();
                ViewData["ObjectMainId"] = new SelectList(objectMainList, "Id", "Description", collection["ObjectMainId"]);
                return View(new ObjectRelationshipEntity());
            }
            catch (Exception ex)
            {
                _logger.LogError("ObjectRelationshipsController - Edit - Error: {0} - StackTrace {1}", ex.Message, ex.StackTrace);
                return Problem("Error editing relationship");
            }            
        }

        #endregion EDIT methods

        #region DELETE methods

        // GET: ObjectRelationships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var objectRelationshipList = await _objectService.GetAllRelationshipsWithParent();
                var objectRelationship = objectRelationshipList.FirstOrDefault(m => m.Id == id);

                if (objectRelationship == null)
                {
                    return NotFound();
                }

                var objectRelationshipModel = _mapper.Map<ObjectRelationshipModel>(objectRelationship);
                return View(objectRelationshipModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("ObjectRelationshipsController - Delete - Error: {0} - StackTrace {1}", ex.Message, ex.StackTrace);
                return Problem("Error deleting relationship");
            }            
        }

        // POST: ObjectRelationships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var objectRelationshipList = await _objectService.GetAllRelationshipsWithParent();
                var objectRelationship = objectRelationshipList.FirstOrDefault(m => m.Id == id);
                if (objectRelationship == null)
                {
                    return Problem("Entity set 'ObjectsContext.ObjectRelationships'  is null.");
                }
                else
                {
                    var mainId = objectRelationship.ObjectMain.Id;
                    _objectService.ObjectRelationshipRemove(objectRelationship);

                    await _objectService.SaveChangesAsync();
                    return RedirectToAction("Edit", "ObjectMains", new { id = mainId });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ObjectRelationshipsController - Delete - Error: {0} - StackTrace {1}", ex.Message, ex.StackTrace);
                return Problem("Error deleting relationship");
            }            
        }

        #endregion DELETE methods

        private bool ObjectRelationshipExists(int id)
        {
            return _objectService.ObjectRelationshipExists(id);
        }
    }
}
