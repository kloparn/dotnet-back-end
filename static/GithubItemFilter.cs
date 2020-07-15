using System.Text.RegularExpressions;
using System;
public static class GithubItemFilter
{
    private static Boolean skip = false;
    public static GithubItem filterRepo(Object repo)
    {
        // Getting the item to fill out for the web api to show later on
        GithubItem item = new GithubItem();

        // A static variable that changes if the current git repo being checked shall be skipped
        skip = false;

        // Converting the 'Object' to a string for maniplulation later on.
        string stringRepo = repo.ToString();

        // This is where the filter starts, looping through every line of object.
        foreach (string str in stringRepo.Split(","))
        {
            // The github api only has 1 '"description"' value in the object, so knowing this we can do a contains to find it.
            if (str.Contains("\"description\""))
            {
                // To make it simpler to understand the str.Replace removes the '"' in the string so it does not make uneeded '"'
                // The str.Substring then later starts the string from where the value is and removes the key value.
                item.description = str.Replace("\"", "").Substring(17);
            }
            else
            {
                string cleanedStr = str.Replace("{", "").Replace("\n", "").Replace("}", "").Replace(" ", "").Replace("\"", "");
                string[] splitStr = cleanedStr.Split(":");

                if (RegexCheck(splitStr[0], RegexCategory.Cancel.Value))
                {
                    if (splitStr.Length > 1)
                    {
                        if (Regex.Match(splitStr[1], @"\btrue\b").Success)
                        {
                            skip = true;
                            continue;
                        }
                    }
                }
                if (RegexCheck(splitStr[0], RegexCategory.Id.Value))
                    if (item.id == 0)
                        item.id = long.Parse(splitStr[1]);
                if (RegexCheck(splitStr[0], RegexCategory.Name.Value))
                    if (item.title == null)
                        item.title = splitStr[1];
                if (RegexCheck(splitStr[0], RegexCategory.Url.Value))
                    if (item.url == null)
                        item.url = cleanedStr.Substring(10) + "/" + item.title;
                if (RegexCheck(splitStr[0], RegexCategory.Created.Value))
                    item.created = Convert.ToDateTime(splitStr[1] + ":" + splitStr[2] + ":" + splitStr[3]);
                if (RegexCheck(splitStr[0], RegexCategory.Updated.Value))
                    item.updated = Convert.ToDateTime(splitStr[1] + ":" + splitStr[2] + ":" + splitStr[3]);
                if (RegexCheck(splitStr[0], RegexCategory.Language.Value))
                    item.language = splitStr[1];

            }
        }
        if (skip)
            return null;
        return item;
    }

    private static Boolean RegexCheck(string str, string regex) { return Regex.Match(str, regex).Success; }
}