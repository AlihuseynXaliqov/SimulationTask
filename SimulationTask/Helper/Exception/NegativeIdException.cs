namespace SimulationTask.Helper.Exception
{
    public class NegativeIdException:System.Exception
    {
        public NegativeIdException():base("Id menfi ve ya sifir ola bilmez") { }
    }
}
