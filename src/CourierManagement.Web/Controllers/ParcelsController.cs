using CourierManagement.Application.Models;
using CourierManagement.Application.Services;
using CourierManagement.Domain.Enums;
using CourierManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourierManagement.Web.Controllers;

public sealed class ParcelsController : Controller
{
    private readonly IParcelService _parcels;
    private readonly IParcelTypeService _parcelTypes;

    public ParcelsController(IParcelService parcels, IParcelTypeService parcelTypes)
    {
        _parcels = parcels;
        _parcelTypes = parcelTypes;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var items = await _parcels.ListAsync(cancellationToken);
        return View(items);
    }

    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var vm = new ParcelCreateVm();
        await PopulateParcelTypesAsync(vm, cancellationToken);
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ParcelCreateVm vm, CancellationToken cancellationToken)
    {
        if (vm.PaymentMethod == PaymentMethod.CashOnDelivery && (vm.CodAmount is null || vm.CodAmount <= 0))
        {
            ModelState.AddModelError(nameof(vm.CodAmount), "COD amount is required for Cash on Delivery.");
        }

        if (!ModelState.IsValid)
        {
            await PopulateParcelTypesAsync(vm, cancellationToken);
            return View(vm);
        }

        var model = new ParcelCreateModel
        {
            ParcelTypeId = vm.ParcelTypeId,
            SenderName = vm.SenderName,
            SenderPhone = vm.SenderPhone,
            SenderAddress = vm.SenderAddress,
            ReceiverName = vm.ReceiverName,
            ReceiverPhone = vm.ReceiverPhone,
            ReceiverAddress = vm.ReceiverAddress,
            WeightKg = vm.WeightKg,
            DeliverySpeed = vm.DeliverySpeed,
            DeliveryPriority = vm.DeliveryPriority,
            PaymentMethod = vm.PaymentMethod,
            CodAmount = vm.PaymentMethod == PaymentMethod.CashOnDelivery ? vm.CodAmount : null
        };

        try
        {
            var parcel = await _parcels.CreateAsync(model, cancellationToken);
            return RedirectToAction(nameof(Invoice), new { id = parcel.Id });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            await PopulateParcelTypesAsync(vm, cancellationToken);
            return View(vm);
        }
    }

    public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken)
    {
        var parcel = await _parcels.GetByIdAsync(id, cancellationToken);
        return parcel is null ? NotFound() : View(parcel);
    }

    public async Task<IActionResult> Invoice(Guid id, CancellationToken cancellationToken)
    {
        var parcel = await _parcels.GetByIdAsync(id, cancellationToken);
        return parcel is null ? NotFound() : View(parcel);
    }

    public IActionResult Track() => View(new TrackVm());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Track(TrackVm vm, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View(vm);

        var parcel = await _parcels.TrackAsync(vm.TrackingId.Trim(), cancellationToken);
        if (parcel is null)
        {
            ModelState.AddModelError(nameof(vm.TrackingId), "Tracking ID not found.");
            return View(vm);
        }

        return RedirectToAction(nameof(Details), new { id = parcel.Id });
    }

    private async Task PopulateParcelTypesAsync(ParcelCreateVm vm, CancellationToken cancellationToken)
    {
        var parcelTypes = await _parcelTypes.ListActiveAsync(cancellationToken);
        vm.ParcelTypes = parcelTypes
            .Select(pt => new SelectListItem(pt.Name, pt.Id.ToString()))
            .ToList();
    }
}

