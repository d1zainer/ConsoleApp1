using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    public class ViewConsol
    {
        public static void EnableMenu()
        {
            Console.WriteLine("Введите цифру для действия. Для выхода нажмите '0'.");
            Console.WriteLine("1 - Получить список пациентов");
            Console.WriteLine("2 - Найти пациента по ID");
            Console.WriteLine("3 - Найти пациента по имени");
            Console.WriteLine("4 - Добавить пациента");
            Console.WriteLine("5 - Обновить пациента");
            Console.WriteLine("6 - Удалить пациента");
            Console.WriteLine("7 - Очистить косноль");
            Console.WriteLine();
        }
        public static void ClearConsol() 
        {
            Console.Clear();
        }
    }
}
