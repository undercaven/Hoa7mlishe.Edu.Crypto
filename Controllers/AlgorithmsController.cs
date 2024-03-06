using Hoa7mlishe.Edu.Crypto.Algorithms;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Hoa7mlishe.Edu.Crypto.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlgorithmsController : ControllerBase
    {
        private readonly ILogger<AlgorithmsController> _logger;

        public AlgorithmsController(ILogger<AlgorithmsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Шифрует сообщение методом RSA
        /// </summary>
        /// <param name="message">сообщение</param>
        /// <param name="num1">простое число 1</param>
        /// <param name="num2">простое число 2</param>
        /// <returns></returns>
        [HttpPost("RSA/encrypt")]
        public IActionResult EncryptRSA(string message, int num1, int num2)
        {
            try
            {
                return Ok(RSA.Encrypt(message, num1, num2));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Расшифровывает сообщение методов RSA
        /// </summary>
        /// <param name="message">сообщение</param>
        /// <param name="privateKey">закрытый ключ</param>
        /// <returns></returns>
        [HttpPost("RSA/decrypt")]
        public IActionResult DecryptRSA(string message, RSA.PrivateKey privateKey)
        {
            return Ok(RSA.Decrypt(message.ToLower(), privateKey));
        }

        /// <summary>
        /// Проверяет корректность номера по алгоритму Луна
        /// </summary>
        /// <param name="number">номер для проверки</param>
        /// <returns></returns>
        [HttpPost("luhn")]
        public IActionResult CheckLuhn(string number)
        {
            return Ok(Luhn.Check(number));
        }

        /// <summary>
        /// Рассчитывает двоичные коды символов сообщения по методу Шеннона-Фано
        /// </summary>
        /// <param name="message">сообщение для расчета</param>
        /// <returns></returns>
        [HttpPost("shannonfano")]
        public IActionResult CalculateShannon(string message)
        {
            return Ok(ShannonFano.Calculate(message));
        }

        /// <summary>
        /// Шифрует строку методом аналитических преобразований
        /// </summary>
        /// <param name="message">строка для шифрования</param>
        /// <param name="matrix">матрица</param>
        /// <returns></returns>
        [HttpPost("analitical")]
        public IActionResult CalculateAnaliticalCoversions(string message, [FromBody] int[][] matrix)
        {
            try
            {
                return Ok(AnaliticalConversions.Encode(message, matrix));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Шифрует сообщение методом виженера
        /// </summary>
        /// <param name="key">ключ</param>
        /// <param name="message">сообщение</param>
        /// <returns></returns>
        [HttpPost("vigener")]
        public IActionResult CalculateVigener(string key, string message)
        {
            return Ok(Vigener.Encrypt(message, key));
        }

        /// <summary>
        /// Шифрует сообщение методом тритемиуса
        /// </summary>
        /// <param name="key">ключ</param>
        /// <param name="message">сообщение</param>
        /// <returns></returns>
        [HttpPost("tritemius")]
        public IActionResult CalculateTritemius(string key, string message)
        {
            return Ok(Tritemius.Calculate(key, message));
        }

        /// <summary>
        /// Строит дерево хаффмена для строки
        /// </summary>
        /// <param name="message">сообщение, для которого требуется построить дерево</param>
        /// <returns></returns>
        [HttpPost("haffman")]
        public IActionResult CalculateHaffman(string message)
        {
            var tree = new HuffmanTree(message);

            return Ok(new { neatArray = tree.EncodeNeatly(message), bitArray = tree.Encode(message)});
        }
    }
}
