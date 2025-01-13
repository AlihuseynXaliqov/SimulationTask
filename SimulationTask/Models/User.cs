using SimulationTask.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimulationTask.Models
{
    public class User:BaseEntity
    {
        public string Name {  get; set; }
        public string ImageUrl { get; set; }

        
        public int positionId { get; set; }
        public Position Position { get; set; }

        
    }
}
