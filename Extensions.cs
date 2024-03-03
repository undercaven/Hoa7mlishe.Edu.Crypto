namespace Hoa7mlishe.Edu.Crypto
{
    public static class Extensions
    {
        public static string FormatMe(this string input) => input.ToLower()
                                                                    .Replace(" ", "")
                                                                    .Replace("й", "и")
                                                                    .Replace("ъ", "ь")
                                                                    .Replace("ё", "е");
    }
}
