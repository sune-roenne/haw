using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Domain.Util;
public static class ByteDataExtensions
{

    private static readonly SHA512 _sha512 = SHA512.Create();
    public static string ToShaHash(this byte[] data) => Convert.ToBase64String(_sha512.ComputeHash(data));


}
