using CourierManagement.Application.Services;
using CourierManagement.Domain.Entities;
using CourierManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourierManagement.Web.Controllers;

public sealed class ParcelTypesController : Controller
{
    private readonly IParcelTypeService _parcelTypes;

    public ParcelTypesController(IParcelTypeService parcelTypes)
    {
        _parcelTypes = parcelTypes;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var items = await _parcelTypes.ListAsync(cancellationToken);
        return View(items.OrderBy(x => x.Name).ToList());
    }

    public IActionResult Create() => View(new ParcelTypeFormVm());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ParcelTypeFormVm vm, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View(vm);

        var entity = new ParcelType
        {
            Name = vm.Name.Trim(),
            Description = string.IsNullOrWhiteSpace(vm.Description) ? null : vm.Description.Trim(),
            BaseRate = vm.BaseRate,
            PerKgRate = vm.PerKgRate,
            IsActive = vm.IsActive
        };

        await _parcelTypes.CreateAsync(entity, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _parcelTypes.GetByIdAsync(id, cancellationToken);
        if (entity is null) return NotFound();

        var vm = new ParcelTypeFormVm
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            BaseRate = entity.BaseRate,
            PerKgRate = entity.PerKgRate,
            IsActive = entity.IsActive
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, ParcelTypeFormVm vm, CancellationToken cancellationToken)
    {
        if (vm.Id != id) return BadRequest();
        if (!ModelState.IsValid) return View(vm);

        var entity = await _parcelTypes.GetByIdAsync(id, cancellationToken);
        if (entity is null) return NotFound();

        entity.Name = vm.Name.Trim();
        entity.Description = string.IsNullOrWhiteSpace(vm.Description) ? null : vm.Description.Trim();
        entity.BaseRate = vm.BaseRate;
        entity.PerKgRate = vm.PerKgRate;
        entity.IsActive = vm.IsActive;

        await _parcelTypes.UpdateAsync(entity, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _parcelTypes.GetByIdAsync(id, cancellationToken);
        return entity is null ? NotFound() : View(entity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _parcelTypes.DeleteAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }
}

