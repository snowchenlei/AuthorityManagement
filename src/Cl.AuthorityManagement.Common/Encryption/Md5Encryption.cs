using Cl.AuthorityManagement.Enum;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Cl.AuthorityManagement.Common.Encryption
{
    public static class Md5Encryption
    {
        #region 非标准MD5密码生成

        private static readonly int A = 9;
        private static readonly int B = 20;
        /// <summary>
        /// MD5密码效验
        /// </summary>
        /// <param name="pwd">加密前的密码</param>
        /// <param name="oldPwd">加密后的密码</param>
        /// <returns>相同返回true，反而反之</returns>
        public static bool Ext_MD5Compare(this string pwd, string oldPwd)
        {
            if (oldPwd.Length < 30)
            {
                return false;
            }
            var s3 = new string[] { oldPwd.Substring(A, 1), oldPwd.Substring(B, 1) };
            var s4 = CreateMD5(s3, pwd);
            return s4.Equals(oldPwd); ;
        }
        /// <summary>
        /// 创建MD5密码
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string Ext_CreateMD5(this string pwd)
        {
            return CreateMD5(rdom(), pwd);
        }
        private static string CreateMD5(string[] rdoms, string pwd)
        {
            string strmd5 = FormsAuthentication.HashPasswordForStoringInConfigFile(rdoms[0] + rdoms[1] + pwd, "MD5");  //把两位随机字母和md5连接并再次进行MD5加密
            return strmd5.Insert(A, rdoms[0]).Insert(B, rdoms[1]);
        }
        /// <summary>
        /// 返回标准MD5
        /// </summary>
        /// <param name="pwd">需要MD5的原始字符串</param>
        /// <returns></returns>
        public static string Ext_StandardMD5(this string pwd)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");
        }
        private static string[] rdom()
        {
            string[] strran = new string[2];
            Random ran = new Random();
            strran[0] = Convert.ToChar(ran.Next(26) + 'a').ToString().ToUpper();
            strran[1] = Convert.ToChar(ran.Next(26) + 'a').ToString().ToUpper();
            return strran;
        }

        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="password">要加密的字符串</param>
        /// <returns></returns>
        public static string Encrypt(string value)
        {
            return Encrypt(value, Md5EncryptionType.Strong);
        }

        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="password">要加密的字符串</param>
        /// <param name="mode">加密强度</param>
        /// <returns></returns>
        public static string Encrypt(string value, Md5EncryptionType mode)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("参数有误");
            }
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            string str = BitConverter.ToString(provider.ComputeHash(Encoding.UTF8.GetBytes(value)));
            provider.Clear();
            if (mode != Md5EncryptionType.Strong)
            {
                return str.Replace("-", null).Substring(8, 0x10);
            }
            return str.Replace("-", null);
        }
        #endregion


        #region AES对称算法




        public static Byte[] StringToByteArray(string str)
        {
            var strList = (str + "").Split(',');

            if (strList.Length < 2)
            {
                return null;
            }
            Byte[] bList = new byte[strList.Length];
            for (int i = 0; i < strList.Length; i++)
            {
                try
                {
                    bList[i] = byte.Parse(strList[i]);
                }
                catch (Exception)
                {

                    return null;
                }
            }
            return bList;
        }

        public static string ByteArrayToString(Byte[] bList)
        {
            if (bList.Length < 1)
            {
                return "";
            }
            var s = "";
            foreach (var item in bList)
            {
                s += item + ",";
            }
            return s.Substring(0, s.Length - 1);
        }
        public static string GetString(byte[] blist)
        {
            return Encoding.UTF8.GetString(blist);
        }
        #endregion
    }
}
