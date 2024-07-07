using Model;
using ConsoleApp1.Services;
using static Model.KeySymbol;
using View;

class Program
{
    readonly static HttpClient httpClient = new HttpClient();
    readonly static string baseAddress = "https://localhost:7166/api/MyApp/";
    readonly static bool flag = true;
    static async Task Main(string[] args)
    {
        KeySymbol keySymbol = new KeySymbol();
        ApiService apiService = new ApiService(httpClient, baseAddress);
        while (flag)
        {
            ViewConsol.EnableMenu();
            var c = Console.ReadKey().KeyChar;
            Console.WriteLine();
            keySymbol.Key = keySymbol.GetKeyType(c).Item1;
 
            if (keySymbol.Keys.TryGetValue(c, out var keyValue))
            {
                keySymbol.Key = keyValue.Item1;
                string endpointSuffix = keyValue.Item2;
                switch (keySymbol.Key)
                {
                    case KeyType.Exit:
                        Console.WriteLine(" - Выход из программы.");
                        return;
                    case KeyType.GetList:
                        Console.WriteLine(" - Получение списка пациентов.");
                        await apiService.GetPatientsAsync(endpointSuffix);
                        break;
                    case KeyType.GetPatientByID:
                        string id;
                        do
                        {
                            Console.Write("GUID пациента: ");
                            id = Console.ReadLine();
                        }
                        while (!Validation.ValidateInputString(id, Validation.InputType.Guid));
                        await apiService.GetPatientByIdAsync(endpointSuffix, Guid.Parse(id));
                        break;

                    case KeyType.GetPatientByName:
                        string name;
                        do
                        {
                            Console.Write("Имя пациента: ");
                            name = Console.ReadLine();
                        } while (!Validation.ValidateInputString(name, Validation.InputType.String));
                        await apiService.GetPatientByNameAsync(endpointSuffix, name);
                        break;

                    case KeyType.AddPatient:
                        Console.WriteLine(" - Добавление нового пациента.");
                        Patient newPatient = new Patient();

                        // ФИО пациента
                        string fullName;
                        do
                        {
                            Console.Write("Имя пациента: ");
                            fullName = Console.ReadLine();
                        } while (!Validation.ValidateInputString(fullName, Validation.InputType.String));

                        newPatient.Fullname = fullName;

                        // Пол пациента (0 - М, 1 - Ж)
                        string gender;
                        do
                        {
                            Console.Write("Пол пациента: ");
                            gender = Console.ReadLine();
                        } while (!Validation.ValidateInputString(gender, Validation.InputType.Int));
                        newPatient.Gender = int.Parse(gender);

                        // Дата рождения пациента (yyyy-MM-ddTHH:mm:ss)
                        string data;
                        do
                        {
                            Console.Write("Дата рождения пациента: ");
                            data = Console.ReadLine();
                        } while (!Validation.ValidateInputString(data, Validation.InputType.DateTime));
                        newPatient.Birthday = DateTime.Parse(data);
                        await apiService.AddPatientAsync(endpointSuffix, newPatient);
                        break;

                    case KeyType.UpdatePatient:
                        Console.WriteLine(" - Обновление данных пациента.");

                        // Запрос ID пациента, чьи данные вы хотите обновить
                        string patientId;
                        do
                        {
                            Console.Write("Введите GUID пациента для обновления: ");
                            patientId = Console.ReadLine();
                        } while (!Validation.ValidateInputString(patientId, Validation.InputType.Guid));
                        _ = keySymbol.Keys.TryGetValue('2', out (KeyType, string) a);


                        // Получение существующих данных пациента
                        Patient existingPatient = await apiService.GetPatientByIdAsync(a.Item2, Guid.Parse(patientId));
                        if (existingPatient == null)
                        {
                            Console.WriteLine($"Пациент с GUID {patientId} не найден.");
                            break;
                        }
                 

                        // Запрос обновленных данных
                        Console.WriteLine("Введите обновленные данные пациента (в формате JSON или по отдельности):");
                        Console.WriteLine("Оставьте поле пустым, чтобы оставить текущее значение.");

                        // ФИО пациента
                        Console.Write($"ФИО пациента ({existingPatient.Fullname}): ");
                        string updatedFullName = Console.ReadLine();
                        if (!string.IsNullOrEmpty(updatedFullName))
                        {
                            existingPatient.Fullname = updatedFullName;
                        }

                        // Пол пациента
                        Console.Write($"Пол пациента ({existingPatient.Gender}): ");
                        string updatedGender = Console.ReadLine();
                        if (!string.IsNullOrEmpty(updatedGender) && Validation.ValidateInputString(updatedGender, Validation.InputType.Int))
                        {
                            existingPatient.Gender = int.Parse(updatedGender);
                        }

                        // Дата рождения пациента
                        Console.Write($"Дата рождения пациента ({existingPatient.Birthday}): ");
                        string updatedBirthday = Console.ReadLine();
                        if (!string.IsNullOrEmpty(updatedBirthday) && Validation.ValidateInputString(updatedBirthday, Validation.InputType.DateTime))
                        {
                            existingPatient.Birthday = DateTime.Parse(updatedBirthday);
                        }

                        // Вызов метода API для обновления пациента
                        await apiService.UpdatePatientAsync(endpointSuffix, existingPatient);
                        break;

                    case KeyType.DeletePatient:
                        Console.WriteLine(" - Удаление пациента.");
                        string deleteGuid;
                        do
                        {
                            Console.Write("GUID пациента: ");
                            deleteGuid = Console.ReadLine();
                        }
                        while (!Validation.ValidateInputString(deleteGuid, Validation.InputType.Guid));
                        await apiService.DeletePatientAsync(endpointSuffix, Guid.Parse(deleteGuid));
                        break;

                    case KeyType.Clear:
                        ViewConsol.ClearConsol();
                        break;
                    default:
                        Console.WriteLine(" - Некорректный ввод. Попробуйте снова.");
                        break; // Используем break вместо return
                }
            }
        }
    }
}