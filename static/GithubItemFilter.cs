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
                item.description = str.Replace("\"", "").Substring(16);
            }
            else
            {
                // Removing the things that's annoying with an key:value object, so '{,},\n, space, \"', making it easier to read through with a regex match.
                string cleanedStr = str.Replace("{", "").Replace("\n", "").Replace("}", "").Replace(" ", "").Replace("\"", "");

                // Now this splits the string to values actually possible to check, for example "name:fob" would split to ["name","fob"]
                string[] splitStr = cleanedStr.Split(":");

                // The regex check to see if the current repository is forked or not.
                if (RegexCheck(splitStr[0], RegexCategory.Cancel.Value))
                {
                    // Checking if the string has more then 1 value, just an error check as the array could only contain 1 string.
                    if (splitStr.Length > 1)
                    {
                        // Now if the value for the key:"fork" is exactly "true" then it will set the skip to True, then continue out of the loop and return a Null value
                        // Instead of a GithubItem
                        if (Regex.Match(splitStr[1], @"\btrue\b").Success)
                        {
                            skip = true;
                            continue;
                        }
                    }
                }

                // The regex check to see if the current string is "id" 
                if (RegexCheck(splitStr[0], RegexCategory.Id.Value))
                    // Checking if the id is set already, as there are 2 ids in a git Repo
                    if (item.id == 0)
                        // Now taking that id string and parsing it to a Long
                        item.id = long.Parse(splitStr[1]);

                // The regex check to see if the current string is "name"
                if (RegexCheck(splitStr[0], RegexCategory.Name.Value))
                    // Checking if the title is set already, as there are more then 1 "name" in a git repo 
                    if (item.title == null)
                        // If all passes then just set the item's title
                        item.title = splitStr[1];

                // The regex check to see if the current string is "url"
                if (RegexCheck(splitStr[0], RegexCategory.Url.Value))
                    // Checking if the url is already set, as there are more then 1 "url" in a git repo
                    if (item.url == null)
                        // Because a url has ":" inside of it, to work around this issue we cut the strings begining and throw the title on at the end 
                        // And thus have a proper link.
                        item.url = cleanedStr.Substring(9) + "/" + item.title;

                // The regex check to see if the current string is "created"
                if (RegexCheck(splitStr[0], RegexCategory.Created.Value))
                    // Converting the string to a Datetime, because it's split i add the proper 'splitness' back together to get a proper input
                    item.created = Convert.ToDateTime(splitStr[1] + ":" + splitStr[2] + ":" + splitStr[3]).ToUniversalTime();

                // The regex check to see if the current string is "updated"
                if (RegexCheck(splitStr[0], RegexCategory.Updated.Value))
                    // Same here as before, i put the string back together for it to convert properly. 
                    item.updated = Convert.ToDateTime(splitStr[1] + ":" + splitStr[2] + ":" + splitStr[3]).ToUniversalTime();

                // The regex check to see if the current string is "language"
                if (RegexCheck(splitStr[0], RegexCategory.Language.Value))
                    // Really simple, just setting the language of the application.
                    item.language = splitStr[1];

                // The regex check to check the homepage for the current Repo
                if (RegexCheck(splitStr[0], RegexCategory.Homepage.Value))
                    // Checking if the website is "null" which means it does not have one at the moment
                    if (splitStr[1] != "null")
                        if (splitStr[1] == "") // Safe check to findout if it's empty, if you save the website as an empty string
                            item.homepage = "null"; // Because it's empty we set it to "null" instead of giving an empty string
                        else item.homepage = splitStr[1] + ":" + splitStr[2]; // Returning the actual homepage for the repo
                    else item.homepage = splitStr[1]; // Returns the "null" value from github.

            }
        }
        // Like before, if the skip Boolean has turned to True, it shall now skip to not include whatever it has gotten.
        if (skip)
            return null;

        // Everything went well, and we can now send the item back for it to be sent to the database.
        return item;
    }

    // To reduce the size of all the checks i made a dynamic check for the Regex.Match, taking two strings with a regex filter then returning the Bool of it.
    private static Boolean RegexCheck(string str, string regex) { return Regex.Match(str, regex).Success; }
}