public record ResultadoDto(bool EsExitoso, string? MsjError)
{
	// Metodos.
	public static ResultadoDto Exitoso() => new(true, null);
	public static ResultadoDto Fallido(string? msjError) => new(false, msjError);
}
