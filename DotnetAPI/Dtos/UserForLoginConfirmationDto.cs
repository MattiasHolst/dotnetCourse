namespace DotnetAPI.Dtos
{
    public partial class UserForLoginConfirmationDto
    {
        byte[] PasswordHash { get; set; } = [0];
        byte[] PasswordSalt { get; set; } = [0];

    }
}