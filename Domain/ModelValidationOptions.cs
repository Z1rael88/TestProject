using Domain.Constants;

namespace Domain;

public class ModelValidationOptions
{
    public int NameMaxLength { get; set; } = 50;
    public int DescriptionMaxLength { get; set; } = 250;
}