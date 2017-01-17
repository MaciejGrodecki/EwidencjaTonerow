using EwidencjaTonerow.Interfaces;
using EwidencjaTonerow.Models;
using System.Web.Mvc;

namespace EwidencjaTonerow.Controllers
{
    public class TonerController : Controller
    {
        private ITonerRepository tonerRepository;

        #region Constructors

        public TonerController()
        {
            EwidencjaContext context = new EwidencjaContext();
            this.tonerRepository = new TonerRepository(context);
        }

        public TonerController(ITonerRepository tonerRepository)
        {
            this.tonerRepository = tonerRepository;
        }

        #endregion Constructors

        #region Actions
        public ActionResult Index()
        {
            var vm = new IndexTonerViewModel();
            vm.Toners = tonerRepository.GetToners();
            return View(vm);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var vw = new AddTonerViewModel();
            return View(vw);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddTonerViewModel vm)
        {
            var toner = new Toner
            {
                Name = vm.Name
            };
            toner.Name = toner.Name.ToUpper();

            if (tonerRepository.IsUniqueName(toner.Name))
            {
                if (ModelState.IsValid)
                {
                    tonerRepository.AddToner(toner);
                    tonerRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("AlreadyExistInformation", "Taki toner już istnieje");
            }
            return View(vm);
        }

        [HttpGet]
        public ActionResult Delete(int TonerID)
        {
            tonerRepository.RemoveToner(TonerID);
            tonerRepository.Save();

            return RedirectToAction("Index");
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            tonerRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}