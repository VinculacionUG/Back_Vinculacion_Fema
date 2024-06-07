namespace Back_Vinculacion_Fema.Models.RequestModels
{
    public class UserLoginRequest
    {
        public string Nombre { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
