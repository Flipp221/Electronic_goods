using System;
using System.Collections.Generic;
using System.Text;

namespace Electronic_goods.Models
{
    public class TovarsService
    {
        public static List<Tovars> lst;

        public static void All()
        {
            lst = new List<Tovars>();
            lst.Add(new Tovars("Товар 1", "Описание товара 1", "7899 P", "Синий", "", "", "", "", DateTime.Now));
            lst.Add(new Tovars("Товар 2", "Описание товара 2", "3789 P", "Чёрный", "", "", "", "", DateTime.Now));
            lst.Add(new Tovars("Товар 3", "Описание товара 3", "2999 P", "Белый", "", "", "", "", DateTime.Now));
            lst.Add(new Tovars("Товар 4", "Описание товара 4", "25999 P", "Розовый", "", "", "", "", DateTime.Now));
        }
    }
}
