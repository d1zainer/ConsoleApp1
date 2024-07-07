public static class Validation
{

    public enum InputType    
    { 
        Guid, 
        String,
        Int,
        DateTime
    
    }
    public static bool ValidateInputString(string input, InputType inputType)
    {
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Строка пустая!");
            return false; // Пустая строка не проходит валидацию
        }
        else
        {
            switch (inputType)
            {
                case InputType.Guid:
                    if (!Guid.TryParse(input, out _))
                    {
                        Console.WriteLine("Некорректный формат GUID.");
                        return false;
                    }
                    return true;
                case InputType.String:
                    if (!System.Text.RegularExpressions.Regex.IsMatch(input, @"^[a-zA-Zа-яА-Я\s]+$"))
                    {
                        Console.WriteLine("Строка должна содержать только буквы и пробелы.");
                        return false;
                    }
                    return true;
                case InputType.Int:
                    if (!int.TryParse(input, out _))
                    {
                        Console.WriteLine("Некорректный формат числа.");
                        return false;
                    }
                    return true;
                case InputType.DateTime:
                    if (!DateTime.TryParseExact(input, "yyyy-MM-ddTHH:mm:ss", null, System.Globalization.DateTimeStyles.None, out _))
                    {
                        Console.WriteLine("Некорректный формат даты.");
                        return false;
                    }
                    return true;
                default:
                    Console.WriteLine("Неизвестный тип ввода.");
                    return false; // Неизвестный тип ввода
            }
        }
    }

}
