namespace LittleTushy.Server
{
    /// <summary>
    /// Options for configuring Little Tushy
    /// </summary>
    public class LittleTushyOptions
    {
        public LittleTushyOptions()
        {
            WebSocketRequestPath = "/lt";
        }
        public string WebSocketRequestPath {get;set;}
    }
}