namespace MeuSiteMVC.Services
{
    public class OperacaoService
    {
        public OperacaoService(IOperacaoTransiente transiente, 
            IOperacaoScoped scoped, IOperacaoSingleton singleton, IOperacaoSingletonInstance singletonInstance)
        {
            Transiente = transiente;
            Scoped = scoped;
            Singleton = singleton;
            SingletonInstance = singletonInstance;
        }

        public IOperacaoTransiente Transiente { get; }
        public IOperacaoScoped Scoped { get; }
        public IOperacaoSingleton Singleton { get; }

        public IOperacaoSingletonInstance SingletonInstance { get; }

    }


    public class Operacao : IOperacaoTransiente,
                            IOperacaoScoped,
                            IOperacaoSingleton,
                            IOperacaoSingletonInstance

    {
        public Operacao() : this(Guid.NewGuid())
        {

        }
        public Operacao(Guid id)
        {
            OperacaoID = id;
        }
        public Guid OperacaoID { get; set; }
    }

    public interface IOperacao
    {
        Guid OperacaoID { get; }
    }

    public interface IOperacaoTransiente : IOperacao
    {

    }

    public interface IOperacaoScoped : IOperacao
    {

    }

    public interface IOperacaoSingleton : IOperacao
    {

    }
    public interface IOperacaoSingletonInstance : IOperacao
    {

    }
}
