
namespace HeladosMaui.Base.DTOs
{
	public record struct OpcionesDeHeladoDto(string Sabor, string Agregado);
	public record HeladoDto(int Id, string Nombre, string Imagen, double Precio, OpcionesDeHeladoDto[] OpcionesDeHeladoDtos);	

}