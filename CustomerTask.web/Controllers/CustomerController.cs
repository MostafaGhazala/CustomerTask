// Project: CustomerTask.Web.Controllers
using CustomerTask.Core.Dtos;
using CustomerTask.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//[Authorize(Roles = "Admin")]
public class CustomerController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    // GET: Customer/Index
    //[Authorize]

    public async Task<ActionResult> Index()
    {
        var customers = await _customerService.GetAllAsync();

        return View(customers);
    }

    // GET: Customer/Create
    //[Authorize]

    public async Task<ActionResult> Create()
    {
        var model = await _customerService.GetCustomerWithLookupsAsync();
        return View(model);
    }

    // POST: Customer/Create
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<ActionResult> Create(CustomerDto model)
    {
        if (ModelState.IsValid)
        {
           var res= await _customerService.CreateCustomerAsync(model);
            return RedirectToAction("Index");
        }
        // If model state is invalid, reload the look-up data before returning to view
        var reloadedModel = await _customerService.GetCustomerWithLookupsAsync(model.Id);
        model.Governorates = reloadedModel.Governorates;
        model.Districts = reloadedModel.Districts;
        model.Villages = reloadedModel.Villages;
        model.Genders = reloadedModel.Genders;

        return View(model);
    }// POST: Customer/TakeNumber
  

    public async Task TakeNumber()
    {
        //get all numbers
        await _customerService.TakeNumberAsync();
    }

    // GET: Customer/Edit/5
    [Authorize]

    public async Task<ActionResult> Edit(int id)
    {
        var model =new CustomerDto();
        // Load the specific customer data AND all dropdown data
         model = await _customerService.GetCustomerWithLookupsAsync(id);

        if (model == null)
        {
            return View("Create", model);
        }
        return View("Create",model);
    }

    // POST: Customer/Edit/5
    [Authorize]

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(CustomerDto model)
    {
        if (ModelState.IsValid)
        {
            await _customerService.UpdateCustomerAsync(model);
            return RedirectToAction("Index");
        }

        // If model state is invalid, reload the look-up data
        var reloadedModel = await _customerService.GetCustomerWithLookupsAsync(model.Id);
        model.Governorates = reloadedModel.Governorates;
        model.Districts = reloadedModel.Districts;
        model.Villages = reloadedModel.Villages;
        model.Genders = reloadedModel.Genders;

        return View(model);
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted = await _customerService.DeleteCustomerAsync(id);

            if (deleted>0)
                return Json(new { success = true, message = "Customer deleted successfully!" });
            else
                return Json(new { success = false, message = "Customer not found or could not be deleted." });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "An error occurred: " + ex.Message });
        }
    }



}