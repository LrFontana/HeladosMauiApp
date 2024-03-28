public record ResultadoConInfoDto<TData>(bool EsExitoso, TData Data, string? MsjError)
{
	// Metodos.
	public static ResultadoConInfoDto<TData> Exitoso(TData data) => new(true, data, null);
	public static ResultadoConInfoDto<TData> Fallido(string? msjError) => new(false, default, msjError);
}
