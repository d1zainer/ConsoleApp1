namespace Model
{
    public class KeySymbol
    {
        public KeyType Key;
        public enum KeyType
        {
            Exit,
            GetList,
            GetPatientByID,
            GetPatientByName,
            AddPatient,
            UpdatePatient,
            DeletePatient,
            Clear
        }

        public Dictionary<char, (KeyType, string)> Keys { get; set; } = new Dictionary<char, (KeyType, string)>()
        {
             { '0', (KeyType.Exit , "exit")},
             { '1', (KeyType.GetList, "GetAllPatient")},
             { '2', (KeyType.GetPatientByID, "GetById")},
             { '3', (KeyType.GetPatientByName, "GetByName")},
             { '4', (KeyType.AddPatient, "AddPatient")},
             { '5', (KeyType.UpdatePatient, "UpdatePatientById")},
             { '6',( KeyType.DeletePatient, "DelPatient")},
             { '7',( KeyType.Clear, "clear")},

        };
        

        public (KeyType, string) GetKeyType(char keySymbol)
        {
            return Keys.GetValueOrDefault(keySymbol);
        }

    }
}
