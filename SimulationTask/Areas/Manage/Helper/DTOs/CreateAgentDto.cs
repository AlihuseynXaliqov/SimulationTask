namespace SimulationTask.Areas.Manage.Helper.DTOs
{
    public record CreateAgentDto
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int positionId { get; set; }
        public IFormFile formFile { get; set; }
    }

}
