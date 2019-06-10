﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Helper
{
    public static class ShoppingList
    {
        public static Dictionary<int, int> boxList = new Dictionary<int, int>();
        public static ServiceResult AddBox(int boxId)
        {

            if (boxList.ContainsKey(boxId))
            {
                //boxList[boxId] += 1;
                return new ServiceResult(ProcessStateEnum.Warning, " isimli ürün çantanızda mevcuttur lütfen miktarını arttırmak için sepetinize gidiniz.");
            }

            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session["sepet"] = boxList;
            UpdateSession();
            boxList.Add(boxId, 1);

            return new ServiceResult(ProcessStateEnum.Success, " isimli ürün çantanıza başarıyla eklenmiştir.");
        }
        public static void RemoveBox(int boxId)
        {
            if (boxList.ContainsKey(boxId))
            {
                boxList[boxId] -= 1;
                UpdateSession();
            }
            else
            {
                boxList.Remove(boxId);
                UpdateSession();
            }
        }
        public static void UpdateSession()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session["sepet"] = boxList;
        }

        public static List<int> GetBoxesKeysInBasket()
        {
            var basket = (Dictionary<int, int>)HttpContext.Current.Session["sepet"];
            List<int> selectedBoxes = new List<int>();
            if (basket.Keys != null)
            {
                selectedBoxes = basket.Keys.ToList();
            }
            return selectedBoxes;
        }

        public static Dictionary<int,int> GetBoxesInBasket()
        {
            var basket = (Dictionary<int, int>)HttpContext.Current.Session["sepet"];
            
            return basket;
        }


    }
}