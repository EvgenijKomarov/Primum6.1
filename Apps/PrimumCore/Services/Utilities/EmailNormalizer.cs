namespace PrimumCore.Services.Utilities
{
    public static class EmailNormalizer
    {
        // Телефоны часто пишут первую букву заглавной и добавляют пробел после автоподстановки
        public static string Normalize(string? email) => (email ?? string.Empty).Trim().ToLowerInvariant();
    }
}
