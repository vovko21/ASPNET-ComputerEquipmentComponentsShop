using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models;

namespace UI.Utils
{
    public class CartItems
    {
        public static List<ComponentViewModel> Items { get; private set; }
        static CartItems()
        {
            Items = new List<ComponentViewModel>();
        }

        public static void AddItem(ComponentViewModel item)
        {
            if (Items == null)
            {
                Items = new List<ComponentViewModel>();
            }

            Items.Add(item);
        }
    }
}