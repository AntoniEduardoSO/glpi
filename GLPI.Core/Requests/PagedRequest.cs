namespace GLPI.Core.Requests;
public abstract class PagedRequest : Request
{
    public int PageNumer {get;set;} = 1;
    public int PageSize {get;set;} = 25;
}
