using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Application.Parsing;
public record ParseResult<TRes>(string Input, TRes Result, int CurrentIndex, string ParsedPart)
{
    public bool HasMore => CurrentIndex + 1 < Input.Length;
}

