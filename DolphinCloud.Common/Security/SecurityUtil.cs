using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Security
{
    /// <summary>
    /// 加解密工具类
    /// </summary>
    public static class SecurityUtil
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="InputText">要加密的文本</param>
        /// <returns></returns>
        public static string MD5Encrypt(string InputText)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(InputText));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }

        public static string MD5Encrypt16(string InputText)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(InputText));
                var strResult = BitConverter.ToString(result, 4, 8);
                return strResult.Replace("-", "");
            }
        }

        /// <summary>
        /// 圆通快递签名
        /// </summary>
        /// <param name="InputText"></param>
        /// <returns></returns>
        public static string YTOSign(string InputText)
        {
            using (var md5 = MD5.Create())
            {
                var utf8Bytes = Encoding.UTF8.GetBytes(InputText);
                var md5Bytes = md5.ComputeHash(utf8Bytes);
                return Convert.ToBase64String(md5Bytes);
            }
        }

        #region DES加密 Create By 王家民 2020-09-12 22:23:59
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="data">待加密原文数据</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">偏移量，ECB模式不用填写！</param>
        /// <param name="algorithm">密文算法</param>
        /// <returns>密文数据</returns>
        public static byte[] DESEncryption(byte[] data, byte[] key, byte[] iv, string algorithm)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (algorithm == null)
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            var cipher = CipherUtilities.GetCipher(algorithm);
            if (iv == null)
            {
                cipher.Init(true, ParameterUtilities.CreateKeyParameter("DES", key));
            }
            else
            {
                cipher.Init(true, new ParametersWithIV(ParameterUtilities.CreateKeyParameter("DES", key), iv));
            }

            return cipher.DoFinal(data);
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="dataXml">传入参数 <see cref="string"/>类型 待加密的明文</param>
        /// <param name="desKey">传入参数 <see cref="string"/>类型 密钥(加密向量)</param>
        /// <returns>返回值 <see cref="string"/>类型 加密之后的密文</returns>
        public static string DESEncryption(string dataXml, string desKey)
        {
            var keyParam = ParameterUtilities.CreateKeyParameter("DES", Convert.FromBase64String(desKey));
            var cipher = (BufferedBlockCipher)CipherUtilities.GetCipher("DES/NONE/PKCS5Padding");

            cipher.Init(true, keyParam);
            var bs = Encoding.UTF8.GetBytes(dataXml);
            var rst = cipher.DoFinal(bs);
            // var asciiBs = Encoding.ASCII.GetBytes(Encoding.UTF8.GetString(rst));
            return Convert.ToBase64String(rst);
        }


        #endregion

        #region DES解密 Create By 王家民 2020-09-12 22:24:51
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="data">待解密数据</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">偏移量，ECB模式不用填写！</param>
        /// <param name="algorithm">密文算法</param>
        /// <returns>未加密原文数据</returns>
        public static byte[] Decrypt(byte[] data, byte[] key, byte[] iv, string algorithm)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var cipher = CipherUtilities.GetCipher(algorithm);
            if (iv == null)
            {
                cipher.Init(false, ParameterUtilities.CreateKeyParameter("DES", key));
            }
            else
            {
                cipher.Init(false, new ParametersWithIV(ParameterUtilities.CreateKeyParameter("DES", key), iv));
            }
            return cipher.DoFinal(data);
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="text">传入参数 <see cref="string"/>类型 待解密的密文</param>
        /// <param name="desKey">传入参数 <see cref="string"/>类型 密钥(加密向量)</param>
        /// <returns>返回值 <see cref="string"/>类型 解密之后的明文</returns>
        public static string DesDecrypt(string text, string desKey)
        {
            var keyParam = ParameterUtilities.CreateKeyParameter("DES", Convert.FromBase64String(desKey));
            var cipher = (BufferedBlockCipher)CipherUtilities.GetCipher("DES/NONE/PKCS5Padding");

            cipher.Init(false, keyParam);
            var bs = Convert.FromBase64String(text);
            var rst = cipher.DoFinal(bs);
            return Encoding.UTF8.GetString(rst);
        }
        #endregion

        #region Base64加密 Create By 王家民 2020-09-12 22:25:52
        /// <summary>
        /// 将字符串转换成base64格式,使用UTF8字符集
        /// </summary>
        /// <param name="content">加密内容</param>
        /// <returns></returns>
        public static string Base64Encode(string content)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            return Convert.ToBase64String(bytes);
        }
        #endregion

        #region Base64解密 Create By 王家民 2020-09-12 22:26:45
        /// <summary>
        /// 将base64格式，转换utf8
        /// </summary>
        /// <param name="content">解密内容</param>
        /// <returns></returns>
        public static string Base64Decode(string content)
        {
            byte[] bytes = Convert.FromBase64String(content);
            return Encoding.UTF8.GetString(bytes);
        }
        #endregion

        #region SHA-1加密 Create By 王家民 2020-09-12 22:31:26
        /// <summary>
        /// SHA-1加密
        /// </summary>
        /// <param name="text">传入参数 <see cref="string"/>类型 需要加密的明文</param>
        /// <returns>返回值 <see cref="string"/>类型 加密之后的SHA-1密文</returns>
        public static string SHA1Encryption(string text)
        {
            Sha1Digest sha1Digest = new Sha1Digest();
            var retValue = new byte[sha1Digest.GetDigestSize()];
            var bs = Encoding.UTF8.GetBytes(text);
            sha1Digest.BlockUpdate(bs, 0, bs.Length);
            sha1Digest.DoFinal(retValue, 0);
            return BitConverter.ToString(retValue).Replace("-", "");
        }
        #endregion

        #region SHA-256加密 Create By 王家民 2020-09-12 22:31:17
        /// <summary>
        /// SHA-256加密
        /// </summary>
        /// <param name="text">传入参数 <see cref="string"/>类型 需要加密的明文</param>
        /// <returns>返回值 <see cref="string"/>类型 加密之后的SHA-256密文</returns>
        public static string SHA256Encryption(string text)
        {
            Sha256Digest sha256Digest = new Sha256Digest();
            var retValue = new byte[sha256Digest.GetDigestSize()];
            var bs = Encoding.UTF8.GetBytes(text);
            sha256Digest.BlockUpdate(bs, 0, bs.Length);
            sha256Digest.DoFinal(retValue, 0);
            return BitConverter.ToString(retValue).Replace("-", "");
        }
        #endregion

        #region 自定义字符串加密与解密

        /// <summary>
        /// 加密明文字符为密文字符串
        /// </summary>
        /// <param name="PlainString">传入参数 <see cref="string"/>类型 需要加密的明文字符串</param>
        /// <returns>返回值 <see cref="string"/>类型 密文字符串</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string EncryptPlainString(string PlainString)
        {
            string CipherString = string.Empty;
            if (string.IsNullOrEmpty(PlainString))
            {
                throw new ArgumentNullException(PlainString, "参数为空");
            }
            var PlainBytes = Encoding.UTF8.GetBytes(PlainString);
            var EncryptBytes = LeCakeEncrypt(PlainBytes);
            CipherString = Encoding.UTF8.GetString(EncryptBytes);
            return CipherString;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="bts"></param>
        /// <returns></returns>
        public static byte[] LeCakeEncrypt(byte[] bts)
        {
            if (bts == null)
            {
                return null;
            }
            if (bts.Length == 0)
            {
                return bts;
            }
            byte[] buff = new byte[bts.Length + 3];
            int tmp_seed = 46;
            int seed = 59;
            int org_len = bts.Length;
            if (org_len > 126)
            {
                return bts;
            }
            int offset = 0;
            buff[(offset++)] = ((byte)tmp_seed);
            if (org_len > 100)
            {
                buff[(offset++)] = ((byte)(int)(new Random().Next() * 9.0D + 1.0D + seed));
                buff[(offset++)] = ((byte)(org_len - 50));
            }
            else
            {
                buff[(offset++)] = ((byte)(seed - 1));
                buff[(offset++)] = ((byte)org_len);
            }
            for (int i = 0; i < bts.Length; i++)
            {
                int bt = 256 + bts[i] & 0xFF;
                bt ^= 0x4;
                buff[(offset++)] = ((byte)bt);
            }
            return buff;
        }

        /// <summary>
        /// 解密安全字符为明文字符
        /// </summary>
        /// <param name="EncryptString">传入参数 <see cref="string"/>类型 加密字符</param>
        /// <returns>返回值 <see cref="string"/>类型 将加密字符解密之后的明文字符</returns>
        public static string DecryptSecurityString(string EncryptString)
        {
            string DecryptStr = string.Empty;
            try
            {
                var EncryptBytes = Encoding.UTF8.GetBytes(EncryptString);
                var DecryptBytes = LeCakeDecrypt(EncryptBytes);
                DecryptStr = Encoding.UTF8.GetString(DecryptBytes);
            }
            catch (Exception)
            {
                throw;
            }
            return DecryptStr;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="bts"></param>
        /// <returns></returns>
        public static byte[] LeCakeDecrypt(byte[] bts)
        {
            if ((bts == null) || (bts.Length < 3))
            {
                return bts;
            }
            int seed = 59;
            byte[] buff = new byte[bts.Length - 3];
            int en_len_flg = bts[1];
            int en_len = 0;
            if (en_len_flg == seed - 1)
            {
                en_len = bts[2];
            }
            else
            {
                en_len = bts[2] + 50;
            }
            if ((en_len < 0) || (en_len > 126))
            {
                return bts;
            }
            int offset = 0;
            for (int i = 3; i < bts.Length; i++)
            {
                int bt = 256 + bts[i] & 0xFF;
                bt ^= 0x4;
                buff[(offset++)] = ((byte)bt);
            }
            return buff;
        }
        #endregion
    }
}
