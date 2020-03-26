namespace DMAnaliseSentimento.Web.API.Model
{
    public class ModelImportacaoDataSet
    {
        public ModelImportacaoDataSet(bool predicado, string texto)
        {
            Predicado = predicado;
            Texto = texto;
        }

        public bool Predicado { get; set; }
        public string Texto { get; set; }
    }
}
