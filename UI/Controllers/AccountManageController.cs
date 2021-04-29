using AutoMapper;
using BLL.Models;
using BLL.Services.Interface;
using DAL.Entity.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UI.Models;
using UI.Utils.IdentityManagers;

namespace UI.Controllers
{
    [Authorize]
    public class AccountManagerController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IStoreService _storeSerice;
        private readonly IMapper _mapper;
        private ApplicationSigninManager SignInManager
        {
            get => HttpContext.GetOwinContext().GetUserManager<ApplicationSigninManager>();
        }
        private ApplicationUserManager UserManager
        {
            get => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public AccountManagerController(IOrderService orderService, IStoreService storeSerice, IMapper mapper)
        {
            _orderService = orderService;
            _storeSerice = storeSerice;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            User user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var resault = await UserManager.ChangePasswordAsync(user.Id, model.CurrentPassword, model.NewPassword);
            if (resault.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Orders()
        {
            var ordersDTO = _orderService.GetAllOrders();
            if (User.IsInRole("Admin"))
            {
                var orderVM = ManualMap(ordersDTO, _mapper.Map<IEnumerable<OrderDTO>, IEnumerable<OrderViewModel>>(ordersDTO));
                if (orderVM.Count() != 0)
                {
                    return View(orderVM);
                }
            }
            else
            {
                var userId = User.Identity.GetUserId();
                var orderVM = ManualMap(ordersDTO.Where(o => o.ClientId == userId), _mapper.Map<IEnumerable<OrderDTO>, IEnumerable<OrderViewModel>>(ordersDTO.Where(o => o.ClientId == userId)));

                if (orderVM.Count() != 0)
                {
                    return View(orderVM);
                }
            }
            
            return View("Index");
        }

        public ActionResult EditOrder(int id)
        {
            var orderDTO = _orderService.GetOrder(id);
            var orderVM = _mapper.Map<OrderDTO, OrderViewModel>(orderDTO);
            foreach (var itemVM in orderVM.OrderItems)
            {
                foreach (var itemDTO in orderDTO.OrderItems)
                {
                    itemVM.Component = _mapper.Map<ComponentDTO, ComponentViewModel>(_storeSerice.GetComponent(itemDTO.ComponentDBId));
                }
            }
            return View(orderVM);
        }

        [HttpPost]
        public async Task<ActionResult> EditOrder(OrderViewModel orderViewModel)
        {
            await _orderService.UpdateOrderAsync(_mapper.Map<OrderViewModel, OrderDTO>(orderViewModel));
            return RedirectToAction("Index");
        }

        public ActionResult DeleteOrder(int id)
        {
            var orderDTO = _orderService.GetOrder(id);
            var orderVM = _mapper.Map<OrderDTO, OrderViewModel>(orderDTO);
            foreach (var itemVM in orderVM.OrderItems)
            {
                foreach (var itemDTO in orderDTO.OrderItems)
                {
                    itemVM.Component = _mapper.Map<ComponentDTO, ComponentViewModel>(_storeSerice.GetComponent(itemDTO.ComponentDBId));
                }
            }
            return View(orderVM);
        }

        [HttpPost, ActionName("DeleteOrder")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var orderDTO = _orderService.GetOrder(id);
            if (orderDTO != null)
            {
                await _orderService.DeleteOrderAsync(id);
            }
            return RedirectToAction("Index");
        }

        public ActionResult OrderDetails(int id)
        {
            var orderDTO = _orderService.GetOrder(id);
            var orderVM = _mapper.Map<OrderDTO, OrderViewModel>(orderDTO);
            foreach (var itemVM in orderVM.OrderItems)
            {
                foreach (var itemDTO in orderDTO.OrderItems)
                {
                    itemVM.Component = _mapper.Map<ComponentDTO, ComponentViewModel>(_storeSerice.GetComponent(itemDTO.ComponentDBId));
                }
            }
            return View(orderVM);
        }

        private IEnumerable<OrderViewModel> ManualMap(IEnumerable<OrderDTO> orderDTO, IEnumerable<OrderViewModel> orderVM)
        {
            foreach (var itemDTO in orderDTO)
            {
                foreach (var orderItemDTO in itemDTO.OrderItems)
                {
                    foreach (var itemVM in orderVM)
                    {
                        foreach (var orderItemVM in itemVM.OrderItems)
                        {
                            orderItemVM.Component = _mapper.Map<ComponentDTO, ComponentViewModel>(_storeSerice.GetComponent(orderItemDTO.ComponentDBId));
                        }
                    }
                }
            }
            return orderVM;
        }
    }
}