using FluentValidation;

namespace SimulationTask.Areas.Manage.Helper.DTOs.Agent
{
    public record CreateAgentDto
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int positionId { get; set; }
        public IFormFile formFile { get; set; }
    }
    public class CreateAgentValidation : AbstractValidator<CreateAgentDto>
    {
        public CreateAgentValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name bos ola bilmez")
                .NotNull().WithMessage("Name null ola bilmez")
                .MaximumLength(20).WithMessage("Name uzunlugu max 20 olmalidi");
        }
    }

}
