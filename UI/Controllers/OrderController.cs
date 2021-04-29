using AutoMapper;
using BLL.Models;
using BLL.Services.Interface;
using DAL.Entity.Enums;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UI.Models;
using UI.Utils.IdentityManagers;

namespace UI.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IStoreService _storeService;
        private readonly IMapper _mapper;
        private List<CookieCartObject> _orderItems;
        private ApplicationUserManager UserManager
        {
            get => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }
        public const string CART = "cart";

        public OrderController(IOrderService orderService, IStoreService storeService, IMapper mapper)
        {
            _orderService = orderService;
            _storeService = storeService;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult OrderForm(List<OrderItemViewModel> orderItems)
        {
            _orderItems = ReadCookie(CART);

            for (int i = 0; i < orderItems.Count; i++)
            {
                _orderItems[i].Quantity = orderItems[i].Quantity;
            }

            RewriteList(_orderItems, CART);

            return Create();
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (_orderItems == null || _orderItems.Count == 0) return RedirectToAction("Index", "ComputerComponent");
            var newOrder = new OrderViewModel { OrderItems = FillOrderItem(ReadCookie(CART)) };
            return View("Create", newOrder);
        }

        [HttpPost]
        public async Task<ActionResult> Create(OrderViewModel order)
        {
            order.ClientId = User.Identity.GetUserId();
            order.OrderCreated = DateTime.Now.Date;
            order.OrderClosed = null;
            order.OrderStatus = OrderStatus.PendingPayment;
            order.OrderItems = FillOrderItem(ReadCookie(CART));

            await _orderService.CreateOrderAsync(_mapper.Map<OrderViewModel, OrderDTO>(order));
            return RedirectToAction("Index", "ComputerComponent");
        }

        private List<OrderItemViewModel> FillOrderItem(List<CookieCartObject> list)
        {
            var orderItems = new List<OrderItemViewModel>();
            foreach (var item in list)
            {
                orderItems.Add(new OrderItemViewModel() { Component = _mapper.Map<ComponentDTO, ComponentViewModel>(_storeService.GetComponent(item.ComponentId)), Quantity = item.Quantity });
            }
            return orderItems;
        }
        private void RewriteList(List<CookieCartObject> list, string key)
        {
            Request.Cookies[key].Value = new JavaScriptSerializer().Serialize(list);
        }

        private List<CookieCartObject> ReadCookie(string key)
        {
            if (Request.Cookies[key] != null && Request.Cookies[key].Value != string.Empty)
            {
                return new JavaScriptSerializer().Deserialize<List<CookieCartObject>>(Request.Cookies[key].Value);
            }
            return null;
        }
    }
}