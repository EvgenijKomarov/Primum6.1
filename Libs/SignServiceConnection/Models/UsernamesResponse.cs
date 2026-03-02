using System;
using System.Collections.Generic;
using System.Text;

namespace SignServiceConnection.Models
{
    // 🔹 Ответ: GET /get-usernames/{userId}
    // Возвращает словарь: realizationTag -> username
    public class UsernamesResponse : Dictionary<string, string> { }
}
