using System;
using System.Collections.Generic;
using System.Text;

namespace Electronic_goods.Models
{
    public class FirnitureService
    {
        public static List<Furniture> lst;

        public static void All()
        {
            lst = new List<Furniture>();
            lst.Add(new Furniture("Товар 1", "Описание товара 1", "7899 P", "Синий", "", "", ""));
            lst.Add(new Furniture("Товар 2", "Описание товара 2", "3789 P", "Чёрный", "", "", ""));
            lst.Add(new Furniture("Товар 3", "Описание товара 3", "2999 P", "Белый", "", "", ""));
            lst.Add(new Furniture("Товар 4", "Описание товара 4", "25999 P", "Розовый", "", "", ""));
        }
    }
}
