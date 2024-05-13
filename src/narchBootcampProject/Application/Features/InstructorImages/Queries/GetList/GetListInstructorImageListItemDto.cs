using NArchitecture.Core.Application.Dtos;

namespace Application.Features.InstructorImages.Queries.GetList;

public class GetListInstructorImageListItemDto : IDto
{
    public int Id { get; set; }
    public Guid InstructorId { get; set; }
    public string ImagePath { get; set; }
}