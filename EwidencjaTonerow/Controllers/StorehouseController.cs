using EwidencjaTonerow.Interfaces;
using EwidencjaTonerow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EwidencjaTonerow.Controllers
{
    public class StorehouseController : Controller
    {
        private IStorehouseRepository storehouseRepository;
        private IOrderRepository orderRepository;
        private IPrinterRepository printerRepository;
        public StorehouseController()
        {
            EwidencjaContext context = new EwidencjaContext();
            this.storehouseRepository = new StorehouseRepository(context);
            this.orderRepository = new OrderRepository(context);
            this.printerRepository = new PrinterRepository(context);
        }

        public StorehouseController(IStorehouseRepository storehouseRepository)
        {
            this.storehouseRepository = storehouseRepository;
        }

        public ActionResult Index(int? orderID, int? printerID, int? roomID,
            int? tonerID)
        {
            var vm = new IndexStorehouseViewModel();
            vm.Printers = printerRepository.GetPrinters();
            vm.Orders = orderRepository.GetOrdersWithoutChangeDate(false);
            if (orderID != null)
            {
                var order = orderRepository.GetOrderByOrderID(orderID.Value);
                order.ChangeDate = DateTime.Now;
                orderRepository.Save();
                storehouseRepository.SubstractToner(printerID.Value, roomID.Value, tonerID.Value);
                storehouseRepository.Save();
            }
            return View(vm);
        }

        public ActionResult Exchanged()
        {
            var vm = new ExchangedStorehouseViewModel();
            vm.Printers = printerRepository.GetPrinters();
            vm.Orders = orderRepository.GetOrderWithChangeDate(false);
            return View(vm);
        }
    }
}