using AutoMapper;
using BLL.Models;
using BLL.Services.Interface;
using BLL.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UI.Models;
using UI.Utils;

namespace UI.Controllers
{
    public class ComputerComponentController : Controller
    {
        private readonly IStoreService storeService;
        private readonly IMapper mapper;

        public ComputerComponentController(IStoreService _componentService, IMapper _mapper)
        {
            storeService = _componentService;
            mapper = _mapper;
            SetViewBag();
        }

        [HttpGet]
        public ActionResult Index(string search)
        {
            var components = mapper.Map<List<ComponentViewModel>>(storeService.GetAllComponents());
            if (String.IsNullOrEmpty(search))
            {
                return View(components.ToList());
            }

            SetViewBag();
            return View(components.Where(x => x.Name.Contains(search)).ToList());
        }

        [HttpPost]
        public async Task<ActionResult> Create(ComponentViewModel model, HttpPostedFileBase uploadFile)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (uploadFile != null)
            {
                var guid = Guid.NewGuid().ToString();
                var fileName = guid + Path.GetExtension(uploadFile.FileName).ToLower();
                var path = Path.Combine(Server.MapPath("/Images/") + fileName);

                uploadFile.SaveAs(path);
                model.Image = path;
            }

            await storeService.CreateComponentAsync(mapper.Map<ComponentDTO>(model));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.TypesName = storeService.GetAllTypeNames();
            ViewBag.ProducersName = storeService.GetAllProducerNames();
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.TypesName = storeService.GetAllTypeNames();
            ViewBag.ProducersName = storeService.GetAllProducerNames();
            return View(mapper.Map<ComponentViewModel>(storeService.GetComponent(id)));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ComponentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await storeService.UpdateComponentAsync(mapper.Map<ComponentDTO>(model));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var component = mapper.Map<ComponentViewModel>(storeService.GetComponent(id));
            if (component == null)
            {
                return HttpNotFound();
            }

            ViewBag.TypesName = storeService.GetAllTypeNames();
            ViewBag.ProducersName = storeService.GetAllProducerNames();
            return View(component);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (mapper.Map<ComponentViewModel>(storeService.GetComponent(id)) == null)
            {
                return HttpNotFound();
            }

            await storeService.DeleteComponentAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Show(int id)
        {
            var component = mapper.Map<ComponentViewModel>(storeService.GetComponent(id));
            if (component == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.TypesName = storeService.GetAllTypeNames();
            ViewBag.ProducersName = storeService.GetAllProducerNames();
            return View(component);
        }

        [HttpPost, ActionName("Show")]
        public ActionResult AddToCart(int id)
        {
            var component = mapper.Map<ComponentViewModel>(storeService.GetComponent(id));
            if (component == null)
            {
                return RedirectToAction("Index");
            }

            CartItems.AddItem(component);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Cart()
        {
            if (CartItems.Items.Count > 0)
            {
                return View(CartItems.Items);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Filter(string type, string value)
        {
            var filter = new ComponentFilter()
            {
                Name = value,
                Type = type
            };

            if (type == "type")
            {
                filter.Predicate = (x => x.Type == value);
            }
            else if (type == "producer")
            {
                filter.Predicate = (x => x.Producer == value);
            }

            var filters = new List<ComponentFilter>();
            if (Session["ComponentFilter"] != null)
            {
                filters = Session["ComponentFilter"] as List<ComponentFilter>;
            }

            var found = filters.FirstOrDefault(f => f.Name == value && f.Type == type);
            if (found != null)
            {
                filters.Remove(found);
            }
            else
            {
                filters.Add(filter);
            }

            if (filters.Count == 0) return HttpNotFound();
            Session["ComponentFilter"] = filters;

            var components = storeService.GetAllComponents(filters);
            SetViewBag();

            return PartialView("ComponentsPartial", mapper.Map<List<ComponentViewModel>>(components));
        }

        private void SetViewBag()
        {
            ViewBag.Types = storeService.GetAllProducerNames();
            ViewBag.Producers = storeService.GetAllTypeNames();
        }
    }
}