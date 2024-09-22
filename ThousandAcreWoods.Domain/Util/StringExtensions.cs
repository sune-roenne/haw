using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Domain.Util;
public static class StringExtensions
{

    public static string ToShaHash(this string str) => Encoding.UTF8.GetBytes(str).ToShaHash();

    public static string CombineHash(this string alreadyHashed, string? nextStr) => $"{alreadyHashed}{nextStr ?? ""}".ToShaHash();

    public static string ToBase64(this string hash) => Convert.ToBase64String(Encoding.UTF8.GetBytes(hash));


    public static string AggregateHash<TItem>(this IEnumerable<TItem> items, string initial, Func<TItem, string> stringer) => 
        items.Any() ? 
        items.Aggregate(initial.ToShaHash(), (hashed, snd) => hashed.CombineHash(stringer(snd).ToShaHash())) :
        initial.ToShaHash();

}
