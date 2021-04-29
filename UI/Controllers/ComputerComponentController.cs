using AutoMapper;
using BLL.Models;
using BLL.Services.Interface;
using BLL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UI.Models;
using UI.Utils;

namespace UI.Controllers
{
    public class ComputerComponentController : Controller
    {
        private readonly IStoreService _storeService;
        private readonly IMapper _mapper;
        private const string CART = "cart";
        private const string VIEWED_PRODUCTS = "ViewedProducts";

        public ComputerComponentController(IStoreService _componentService, IMapper _mapper)
        {
            _storeService = _componentService;
            this._mapper = _mapper;
            SetViewBag();
        }

        [HttpGet]
        public ActionResult Index(string search)
        {
            var components = _mapper.Map<IEnumerable<ComponentDTO>, IEnumerable<ComponentViewModel>>(_storeService.GetAllComponents());
            if (String.IsNullOrEmpty(search))
            {
                var viewedProducts = GetSessionList<ViewedList>(VIEWED_PRODUCTS);
                if (viewedProducts == null)
                {
                    return View(new IndexViewModel() { Component = components.ToList() });
                }
                else
                {
                    return View(new IndexViewModel() { Component = components.ToList(), ViewedList = viewedProducts });
                }
            }

            SetViewBag();

            return View(new IndexViewModel() { Component = components.Where(x => x.Name.Contains(search)).ToList() });
        }

        [HttpGet]
        public ActionResult Show(int id)
        {
            var component = _mapper.Map<ComponentDTO, ComponentViewModel>(_storeService.GetComponent(id));
            if (component == null)
            {
                return RedirectToAction("Index");
            }

            var list = GetSessionList<ViewedList>(VIEWED_PRODUCTS);
            if (list == null)
            {
                var newList = new ViewedList();
                newList.Append(component);
                SaveToSession(newList, VIEWED_PRODUCTS);
            }
            else
            {
                list.Append(component);
                SaveToSession(list, VIEWED_PRODUCTS);
            }

            ViewBag.TypesName = _storeService.GetAllTypeNames();
            ViewBag.ProducersName = _storeService.GetAllProducerNames();

            return View(component);
        }

        [HttpPost, ActionName("Show")]
        public ActionResult AddToCart(int id)
        {
            var component = _mapper.Map<ComponentDTO, ComponentViewModel>(_storeService.GetComponent(id));

            if (component == null)
            {
                return RedirectToAction("Index");
            }

            WriteCookie(component);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Cart()
        {
            var list = ReadCookie(CART);

            if (list != null)
            {
                var orderItems = new List<OrderItemViewModel>();
                foreach (var item in list)
                {
                    orderItems.Add(new OrderItemViewModel() { Component = _mapper.Map<ComponentDTO, ComponentViewModel>(_storeService.GetComponent(item.ComponentId)), Quantity = item.Quantity });
                }
                return View(orderItems);
            }

            return RedirectToAction("Index");
        }

        private void WriteCookie(ComponentViewModel component)
        {
            List<CookieCartObject> listToSave;

            var cookieObject = new CookieCartObject { ComponentId = component.Id, Quantity = 1 };

            var oldCookies = ReadCookie(CART);

            if (oldCookies == null)
            {
                List<CookieCartObject> list = new List<CookieCartObject>();
                list.Add(cookieObject);
                listToSave = list;
            }
            else
            {
                var cartComponent = _mapper.Map<ComponentDTO, ComponentViewModel>(_storeService.GetComponent(component.Id));
                var thisComponent = oldCookies.Find(x => x.ComponentId == cartComponent.Id);

                if (thisComponent == null)
                {
                    oldCookies.Add(cookieObject);
                }
                else
                {
                    oldCookies.Remove(thisComponent);
                    thisComponent.Quantity++;
                    oldCookies.Add(thisComponent);
                }
                listToSave = oldCookies;
            }

            HttpCookie httpCookie = new HttpCookie(CART);

            httpCookie.Value = new JavaScriptSerializer().Serialize(listToSave);
            httpCookie.Expires = DateTime.Now.AddDays(30d);
            HttpContext.Response.Cookies.Add(httpCookie);
        }

        private List<CookieCartObject> ReadCookie(string key)
        {
            if (Request.Cookies[key] != null && Request.Cookies[key].Value != string.Empty)
            {
                return new JavaScriptSerializer().Deserialize<List<CookieCartObject>>(Request.Cookies[key].Value);
            }
            return null;
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

            Session["ComponentFilter"] = filters;
            // BUG to fix [start] -> 
            if (filters.Count == 0) return RedirectToAction("Index");
            // <- [end]
            List<ComponentViewModel> components = _mapper.Map<List<ComponentDTO>, List<ComponentViewModel>>(_storeService.GetAllComponents(filters).ToList());

            SetViewBag();

            if (components.Count != 0)
            {
                return PartialView("ComponentsPartial", components);
            }

            return HttpNotFound();
        }

        private void SaveToSession<T>(T list, string name)
        {
            Session[name] = list;
        }

        private T GetSessionList<T>(string name)
        {
            if (Session[name] == null)
            {
                return default(T);
            }

            if (Session[name] is T)
            {
                return (T)Session[name];
            }

            return default(T);
        }

        private void SetViewBag()
        {
            ViewBag.Types = _storeService.GetAllTypeNames();
            ViewBag.Producers = _storeService.GetAllProducerNames();
        }
    }
}