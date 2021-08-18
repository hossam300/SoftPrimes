namespace SoftPrimes.Shared.Domains
{
    public class AgentRole
    {
        public int Id { get; set; }
        public string AgentId { get; set; }
        public virtual Agent Agent { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}