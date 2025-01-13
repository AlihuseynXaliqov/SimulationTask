using SimulationTask.Models.Base;

namespace SimulationTask.Models
{
    public class Position:BaseEntity
    {
        public string PositionName {  get; set; }
        public List<User> Users { get; set; }
    }
}
