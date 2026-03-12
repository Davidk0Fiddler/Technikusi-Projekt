namespace RetroRealm_Server.WebSocket
{
    public class WsMessage<T>
    {
        public string Type { get; set; }
        public T Data { get; set; }
    }
}
