namespace LittleTushy
{
    public class LittleTushyOptions
    {
        public LittleTushyOptions()
        {
            WebSocketRequestPath = "/lt";
        }
        public string WebSocketRequestPath {get;set;}
    }
}