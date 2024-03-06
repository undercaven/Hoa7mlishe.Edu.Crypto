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
        /// ������� ��������� ������� RSA
        /// </summary>
        /// <param name="message">���������</param>
        /// <param name="num1">������� ����� 1</param>
        /// <param name="num2">������� ����� 2</param>
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
        /// �������������� ��������� ������� RSA
        /// </summary>
        /// <param name="message">���������</param>
        /// <param name="privateKey">�������� ����</param>
        /// <returns></returns>
        [HttpPost("RSA/decrypt")]
        public IActionResult DecryptRSA(string message, RSA.PrivateKey privateKey)
        {
            return Ok(RSA.Decrypt(message.ToLower(), privateKey));
        }

        /// <summary>
        /// ��������� ������������ ������ �� ��������� ����
        /// </summary>
        /// <param name="number">����� ��� ��������</param>
        /// <returns></returns>
        [HttpPost("luhn")]
        public IActionResult CheckLuhn(string number)
        {
            return Ok(Luhn.Check(number));
        }

        /// <summary>
        /// ������������ �������� ���� �������� ��������� �� ������ �������-����
        /// </summary>
        /// <param name="message">��������� ��� �������</param>
        /// <returns></returns>
        [HttpPost("shannonfano")]
        public IActionResult CalculateShannon(string message)
        {
            return Ok(ShannonFano.Calculate(message));
        }

        /// <summary>
        /// ������� ������ ������� ������������� ��������������
        /// </summary>
        /// <param name="message">������ ��� ����������</param>
        /// <param name="matrix">�������</param>
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
        /// ������� ��������� ������� ��������
        /// </summary>
        /// <param name="key">����</param>
        /// <param name="message">���������</param>
        /// <returns></returns>
        [HttpPost("vigener")]
        public IActionResult CalculateVigener(string key, string message)
        {
            return Ok(Vigener.Encrypt(message, key));
        }

        /// <summary>
        /// ������� ��������� ������� ����������
        /// </summary>
        /// <param name="key">����</param>
        /// <param name="message">���������</param>
        /// <returns></returns>
        [HttpPost("tritemius")]
        public IActionResult CalculateTritemius(string key, string message)
        {
            return Ok(Tritemius.Calculate(key, message));
        }

        /// <summary>
        /// ������ ������ �������� ��� ������
        /// </summary>
        /// <param name="message">���������, ��� �������� ��������� ��������� ������</param>
        /// <returns></returns>
        [HttpPost("haffman")]
        public IActionResult CalculateHaffman(string message)
        {
            var tree = new HuffmanTree(message);

            return Ok(new { neatArray = tree.EncodeNeatly(message), bitArray = tree.Encode(message)});
        }
    }
}
