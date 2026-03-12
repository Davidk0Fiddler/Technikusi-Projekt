namespace RetroRealm_Server.DTOs.AdminPanelDTOs
{
    public class ReadLogDTO
    {
        public string LogType { get; set; }
        public string Error { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public string UserName { get; set; }
    }
}
