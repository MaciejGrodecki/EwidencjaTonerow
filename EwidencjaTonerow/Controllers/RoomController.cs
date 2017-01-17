using EwidencjaTonerow.Interfaces;
using EwidencjaTonerow.Models;
using System.Web.Mvc;

namespace EwidencjaTonerow.Controllers
{
    public class RoomController : Controller
    {
        private IRoomRepository roomRepository;

        #region Contructors

        public RoomController()
        {
            EwidencjaContext context = new EwidencjaContext();
            this.roomRepository = new RoomRepository(context);
        }

        public RoomController(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }

        #endregion Contructors

        #region Actions

        public ActionResult Index()
        {
            var vm = new IndexRoomViewModel();
            vm.Rooms = roomRepository.GetRooms();
            return View(vm);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var vm = new AddRoomViewModel();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddRoomViewModel vm)
        {
            var room = new Room
            {
                Name = vm.Name
            };
            room.Name = room.Name.ToUpper();
            if (roomRepository.IsUniqueName(room.Name))
            {
                if (ModelState.IsValid)
                {
                    roomRepository.AddRoom(room);
                    roomRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("AlreadyExistInformation", "Takie pomieszczenie już istnieje");
            }
            return View(vm);
        }

        [HttpGet]
        public ActionResult Delete(int RoomID)
        {
            roomRepository.RemoveRoom(RoomID);
            roomRepository.Save();

            return RedirectToAction("Index");
        }

        #endregion Actions

        protected override void Dispose(bool disposing)
        {
            roomRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}