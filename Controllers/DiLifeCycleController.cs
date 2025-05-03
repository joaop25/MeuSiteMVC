using MeuSiteMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeuSiteMVC.Controllers
{
    [Route("teste-di")]
    public class DiLifeCycleController : Controller
    {
        public OperacaoService OperacaoService { get; }

        public OperacaoService OperacaoService2 { get; }

        public DiLifeCycleController(OperacaoService operacaoService,
            OperacaoService operacaoService2)
        {
            OperacaoService = operacaoService;
            OperacaoService2 = operacaoService2;
        }
        public string Index()
        {
            return
                "Primeira instância: " + Environment.NewLine +
                OperacaoService.Transiente.OperacaoID + Environment.NewLine +
                OperacaoService.Scoped.OperacaoID + Environment.NewLine +
                OperacaoService.Singleton.OperacaoID + Environment.NewLine +
                OperacaoService.SingletonInstance.OperacaoID + Environment.NewLine +

                Environment.NewLine +
                Environment.NewLine +

                "Segunda instância: " + Environment.NewLine +
                OperacaoService2.Transiente.OperacaoID + Environment.NewLine +
                OperacaoService2.Scoped.OperacaoID + Environment.NewLine +
                OperacaoService2.Singleton.OperacaoID + Environment.NewLine +
                OperacaoService2.SingletonInstance.OperacaoID + Environment.NewLine;

        }

        
    }
}
