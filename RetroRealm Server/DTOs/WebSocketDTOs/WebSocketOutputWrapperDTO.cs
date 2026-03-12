namespace RetroRealm_Server.DTOs.WebSocketDTOs
{
    public class WebSocketOutputWrapperDTO
    {
        public string YourUserName { get; set; }
        public List<WebSocketOutputDTO> Players { get; set; }
    }
}
