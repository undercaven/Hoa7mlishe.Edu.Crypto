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
        /// Шифрует сообщение методом тритемиуса
        /// </summary>
        /// <param name="key">ключ. длина 6 символов</param>
        /// <param name="message">сообщение. длина от 10 до 25 символов</param>
        /// <returns></returns>
        [HttpPost("tritemius")]
        public IActionResult CalculateTritemius(string key, string message)
        {
            try
            {
                return Ok(Tritemius.Calculate(key, message));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
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
