namespace Hoa7mlishe.Edu.Crypto.Algorithms
{
    public static class Luhn
    {
        public static string Check(string number)
        {
            int[] digits = number
                .Select(ch => int.Parse(ch.ToString()))
                .ToArray();

            int checkDigit = 0;
            for (int i = digits.Length - 2; i >= 0; --i)
                checkDigit += ((i & 1) is 0) switch
                {
                    true => digits[i] > 4 ? digits[i] * 2 - 9 : digits[i] * 2,
                    false => digits[i]
                };

            return 10 - (checkDigit % 10) == digits.Last() ? "Номер корректен" : "Номер некорректен";
        }
    }
}
