
public class RegexCategory
{
    private RegexCategory(string value) { Value = value; }

    public string Value { get; set; }
    public static RegexCategory Id { get { return new RegexCategory(new string(@"\bid\b")); } }
    public static RegexCategory Name { get { return new RegexCategory(new string(@"\bname\b")); } }
    public static RegexCategory Url { get { return new RegexCategory(new string(@"\bhtml_url\b")); } }
    public static RegexCategory Created { get { return new RegexCategory(new string(@"\bcreated_at\b")); } }
    public static RegexCategory Updated { get { return new RegexCategory(new string(@"\bupdated_at\b")); } }
    public static RegexCategory Language { get { return new RegexCategory(new string(@"\blanguage\b")); } }
    public static RegexCategory Cancel { get { return new RegexCategory(new string(@"\bfork\b")); } }
    public static RegexCategory Homepage { get { return new RegexCategory(new string(@"\bhomepage\b")); } }
}