using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Domain.Book.Model;
public record BookStoryLine(
    string StoryLine,
    string StoryLineName,
    string Image,
    string Video,
    string SiteFont,
    BookStoryLineAttribution Attribution)
{
    public string StoryLineKey => StoryLine.Trim().ToLower();
};


public record BookStoryLineAttribution(
    string Text,
    string Link
    );
