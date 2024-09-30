Public Interface IDAO(Of T As Class)
	Function Inserir(obj As T) As Integer
	Sub Editar(obj As T, idObjASerEditado As Integer)
	Function ListarTodos() As List(Of T)
	Function ObterPorId(id As Integer) As T
	Function ConverterReaderParaListaDeObjetos(reader As IEnumerable(Of IDataRecord)) As List(Of T)
End Interface
