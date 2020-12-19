namespace WebAPI.Helpers
{
    public class UserParams : PaginationParams
    {
        // Property for filtering
        public string CurrentUsername { get; set; }
        public string Gender { get; set; }
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 150;

        // Property for ordering
        public string OrderBy { get; set; } = "lastActive";
    }
}