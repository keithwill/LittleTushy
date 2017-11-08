namespace LittleTushy.Server
{
    /// <summary>
    /// Marks a ServiceController method as being a service operation
    /// A ServiceController action must be an 
    /// async Task&lt;ServiceResponse&lt;T&gt;&gt; or
    /// async Task&lt;StatusResponse&gt;
    /// </summary>
    public class ActionAttribute : System.Attribute
    {
    }
}