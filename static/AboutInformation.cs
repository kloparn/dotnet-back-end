
using TodoApi.Models;
public static class AboutInformation
{
    public static InformationItem Frontend(InformationContext context)
    {
        InformationItem item = new InformationItem();
        item.Id = context.InformationItems.Local.Count + 1;
        item.Name = "Frontend";
        item.Information = "The front-end is built in React.TS. A reponsive and flexable framework to develop in and make it easy to implement single-page or mobile applications.";
        item.Library = "React";
        item.Language = "Typescript";
        return item;
    }
    public static InformationItem User(InformationContext context)
    {
        InformationItem item = new InformationItem();
        item.Id = context.InformationItems.Local.Count + 1;
        item.Name = "Adam";
        item.Information = "Graduated from Jönköping University. Graduated in Computer science with a focus on Software development and mobile platforms.";
        return item;
    }
    public static InformationItem Backend(InformationContext context)
    {
        InformationItem item = new InformationItem();
        item.Id = context.InformationItems.Local.Count + 1;
        item.Name = "Backend";
        item.Information = "The backend has actually been converted to ASP.NET to further increase my skills in the enviroment of C#, but also to make it a little bit more flexable with maintenance";
        item.Framework = "ASP.NET Core";
        item.Language = "C#";
        return item;
    }
    public static InformationItem VueFrontend(InformationContext context)
    {
        InformationItem item = new InformationItem();
        item.Id = context.InformationItems.Local.Count + 1;
        item.Name = "Vue Frontend";
        item.Information = "Vue made frontend to showcase my expertise in the Vue framework, further development in the Vue framework will be done here.";
        item.Framework = "Vue";
        item.Language = "Vue/Javascript";
        return item;
    }
}