using EwidencjaTonerow.Interfaces;
using EwidencjaTonerow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EwidencjaTonerow.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository orderRepository;
        private IPrinterRepository printerRepository;
        private IRoomRepository roomRepository;
        private ITonerRepository tonerRepository;
        private IStorehouseRepository storehouseRepository;

        #region Constructors

        public OrderController()
        {
            EwidencjaContext context = new EwidencjaContext();
            this.orderRepository = new OrderRepository(context);
            this.printerRepository = new PrinterRepository(context);
            this.roomRepository = new RoomRepository(context);
            this.tonerRepository = new TonerRepository(context);
            this.storehouseRepository = new StorehouseRepository(context);
        }

        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        #endregion Constructors

        #region Actions


        public ActionResult Index()
        {
            var vm = new IndexOrderViewModel();
            vm.Orders = orderRepository.GetOrdersWithoutDeliveryDate(true);
            ViewBag.HeaderTitle = "Aktualne zamówienia";

            return View(vm);
        }

        public ActionResult Delivered()
        {
            var vm = new DeliveredOrderViewModel();
            vm.Orders = orderRepository.GetOrdersWithDeliveryDate(true);
            ViewBag.HeaderTitle = "Dostarczone zamówienia";

            return View(vm);
        }

        /*
        public ActionResult Index(int? deliveryOrderID, bool? doneOrders, int? printerID, 
            int? roomID, int? tonerID)
        {
            var vm = new IndexOrderViewModel();
            if (doneOrders != null)
            {
                if (!doneOrders.Value)
                {
                    vm.Orders = orderRepository.GetOrdersWithoutDeliveryDate(true);
                    ViewBag.HeaderTitle = "Aktualne zamówienia";
                }
                else
                {
                    vm.Orders = orderRepository.GetOrdersWithDeliveryDate(true);
                    ViewBag.HeaderTitle = "Dostarczone zamówienia";
                }
            }
            else
            {
                vm.Orders = orderRepository.GetOrdersWithoutDeliveryDate(true);
                ViewBag.HeaderTitle = "Aktualne zamówienia";
            }

            if (deliveryOrderID != null)
            {
                var order = orderRepository.GetOrderByOrderID(deliveryOrderID.Value);
                order.DeliveryDate = DateTime.Now;
                orderRepository.Save();
                storehouseRepository.AddToner(printerID.Value, roomID.Value, tonerID.Value);
                storehouseRepository.Save();
                vm.Orders = orderRepository.GetOrdersWithoutDeliveryDate(true);
                ViewBag.HeaderTitle = "Aktualne zamówienia";
            }
            return View(vm);
        }
        */
        [HttpGet]
        public ActionResult Add()
        {
            var vm = new AddOrderViewModel();
            vm.Printers = printerRepository.GetPrinters();
            vm.Rooms = new List<Room>();
            vm.Toners = new List<Toner>();
            vm.SendDate = DateTime.Now;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddOrderViewModel vm)
        {

            var order = new Order
            {
                StorehouseID = storehouseRepository.GetStorehouseID(vm.PrinterID, vm.RoomID,
                vm.TonerID),
                SendDate = vm.SendDate
            };


            if (ModelState.IsValid)
            {
                orderRepository.AddOrder(order);
                orderRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View(vm);
            }
        }

        #endregion

        #region Json

        [HttpPost]
        public JsonResult GetPrinterRooms(int printerID)
        {
            Printer printer = printerRepository.GetPrinterByPrinterID(printerID);
            var rooms = new List<SelectListItem>();
            rooms.Add(new SelectListItem());

            if (printer.Rooms != null)
            {
                foreach (var r in printer.Rooms)
                {
                    rooms.Add(
                        new SelectListItem
                        {
                            Text = r.Name,
                            Value = r.RoomID.ToString()
                        });
                }
            }
            return Json(new SelectList(rooms, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        [HttpPost]
        public JsonResult GetPrinerToners(int printerID)
        {
            Printer printer = printerRepository.GetPrinterByPrinterID(printerID);
            var toners = new List<SelectListItem>();
            toners.Add(new SelectListItem());

            if (printer.Toners != null)
            {
                foreach (var t in printer.Toners)
                {
                    toners.Add(
                        new SelectListItem
                        {
                            Text = t.Name,
                            Value = t.TonerID.ToString()
                        });
                }
            }
            return Json(new SelectList(toners, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        #endregion
    }
}