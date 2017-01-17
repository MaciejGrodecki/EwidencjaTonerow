using EwidencjaTonerow.Interfaces;
using EwidencjaTonerow.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EwidencjaTonerow.Controllers
{
    public class PrinterController : Controller
    {
        private IPrinterRepository printerRepository;
        private IRoomRepository roomRepository;
        private ITonerRepository tonerRepository;
        private IOrderRepository orderRepository;
        private IStorehouseRepository storehouseRepository;

        #region Constructors

        public PrinterController()
        {
            EwidencjaContext context = new EwidencjaContext();
            this.printerRepository = new PrinterRepository(context);
            this.roomRepository = new RoomRepository(context);
            this.tonerRepository = new TonerRepository(context);
            this.orderRepository = new OrderRepository(context);
            this.storehouseRepository = new StorehouseRepository(context);
        }

        public PrinterController(IPrinterRepository printerRepository)
        {
            this.printerRepository = printerRepository;
        }

        public PrinterController(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }

        public PrinterController(ITonerRepository tonerRepository)
        {
            this.tonerRepository = tonerRepository;
        }
        public PrinterController(IStorehouseRepository storehouseRepository)
        {
            this.storehouseRepository = storehouseRepository;
        }


        #endregion Constructors

        #region Actions

        public ActionResult Index(int? PrinterID, int? PrinterTonerID)
        {
            var vm = new IndexPrinterViewModel();
            vm.Printers = printerRepository.GetPrinters();

            if (PrinterID != null)
            {
                ViewBag.PrinterID = PrinterID.Value;
                vm.Rooms = roomRepository.GetPrinterRooms(PrinterID.Value, vm.Printers);
            }

            if (PrinterTonerID != null)
            {
                ViewBag.PrinterID = PrinterTonerID.Value;
                vm.Toners = tonerRepository.GetPrinterToners(PrinterTonerID.Value, vm.Printers);
            }

            return View(vm);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var vw = new AddPrinterViewModel();
            return View(vw);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddPrinterViewModel vm)
        {
            if (printerRepository.IsUniqueName(vm.Name))
            {
                var printer = new Printer
                {
                    Name = vm.Name
                };
                printer.Name = printer.Name.ToUpper();

                if (ModelState.IsValid)
                {
                    printerRepository.AddPrinter(printer);
                    printerRepository.Save();
                    storehouseRepository.Add(
                        new Storehouse
                        {
                            PrinterID = printer.PrinterID,

                        });
                    storehouseRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("AlreadyExistInformation", "Taka drukarka już istnieje");
            }

            return View(vm);
        }

        [HttpGet]
        public ActionResult Delete(int printerID)
        {
            printerRepository.RemovePrinter(printerID);
            printerRepository.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddRoomToPrinter(int PrinterID)
        {
            var vw = new AddRoomToPrinterViewModel();
            vw.Printer = printerRepository.GetPrinterByPrinterID(PrinterID);
            vw.Rooms = roomRepository.GetRooms();

            foreach (Room r in vw.Printer.Rooms)
            {
                vw.Rooms.Remove(r);
            }

            return View(vw);
        }

        [HttpPost]
        public ActionResult AddRoomToPrinter(AddRoomToPrinterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Printer = printerRepository.GetPrinterByPrinterID(vm.PrinterID);
                vm.Printer.Rooms.Add(roomRepository.GetRoomByRoomID(vm.RoomID));
                Storehouse storehouseItem = storehouseRepository.GetItemByPrinterIdWithoutRoom(vm.PrinterID);
                if(storehouseItem != null)
                {
                    storehouseItem.RoomID = vm.RoomID;
                    storehouseRepository.Save();
                }
                else
                {
                    storehouseItem = new Storehouse();
                    storehouseItem.PrinterID = vm.PrinterID;
                    storehouseItem.RoomID = vm.RoomID;
                    storehouseRepository.Fill(storehouseItem);
                    storehouseRepository.Save();
                }
                printerRepository.Save();
                roomRepository.Save();
                return RedirectToAction("Index", new
                {
                    PrinterID = vm.PrinterID
                });
            }

            return View(vm);
        }

        [HttpGet]
        public ActionResult AddTonerToPrinter(int PrinterID)
        {
            var vm = new AddTonerToPrinterViewModel();
            vm.Printer = printerRepository.GetPrinterByPrinterID(PrinterID);
            vm.Toners = tonerRepository.GetToners();

            foreach (Toner t in vm.Printer.Toners)
            {
                vm.Toners.Remove(t);
            }
            return View(vm);
        }

        [HttpPost]
        public ActionResult AddTonerToPrinter(AddTonerToPrinterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Printer = printerRepository.GetPrinterByPrinterID(vm.PrinterID);
                vm.Printer.Toners.Add(tonerRepository.GetTonerByTonerID(vm.TonerID));
                ICollection<Storehouse> storehouseItems = storehouseRepository.GetItemByPrinterIdWithoutToner(vm.PrinterID);
                if(storehouseItems.Count > 0)
                {
                    foreach (var item in storehouseItems)
                    {
                        item.TonerID = vm.TonerID;
                        storehouseRepository.Save();
                    }
                }
                else
                {
                    Storehouse storehouseItem = new Storehouse();
                    storehouseItem.PrinterID = vm.PrinterID;
                    storehouseItem.TonerID = vm.TonerID;
                    storehouseRepository.Fill(storehouseItem);
                }
                printerRepository.Save();
                tonerRepository.Save();
                return RedirectToAction("Index", new
                {
                    PrinterTonerID = vm.PrinterID
                });
            }
            return View(vm);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            printerRepository.Dispose();
            tonerRepository.Dispose();
            roomRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}