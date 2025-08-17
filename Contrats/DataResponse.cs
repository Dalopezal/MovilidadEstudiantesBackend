namespace Apis.Contrats
{
    public class DataResponse<T>
    {
        public required string Exito { get; set; }
        public required T Datos { get; set; }
    }

}
