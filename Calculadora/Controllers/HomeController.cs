using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Calculadora.Models;

namespace Calculadora.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// invocação da calculadora, em modo HttpGET
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //enviar o valor inicial para o visor
            ViewBag.Visor = "0";
            ViewBag.Operador = "";
            ViewBag.PrimeiroOperando = "";
            ViewBag.LimpaVisor = true + "";

            return View();
        }


        /// <summary>
        /// apresentação da calculadora, em modo HttpPOST
        /// </summary>
        /// <param name="bt">valor do botão premido pelo utilizador</param>
        /// <param name="visor">valor visivel no visor da calculadora e utilizado como segundo operando na operação escolhida</param>
        /// <param name="operador">operador a ser utilizado na operação escolhida na calculadora</param>
        /// <param name="primeiroOperando">primeiro operando utilizado na opearação</param>
        /// <param name="limpaVisor">limpa o visor para receber novo valor</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(
            string bt,
            string visor,
            string operador,
            string primeiroOperando,
            bool limpaVisor)
        {
             
            switch (bt)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":
                    //construir o 'numero' do visor
                    if (visor == "0" || limpaVisor) visor = bt;
                    else visor = visor + bt; // visor += bt;
                    //avisa para não limpar o visor
                    limpaVisor = false;
             break;

                case "+/-":
                    //efetuar a inversao do valor do visor
                    visor = Convert.ToDouble(visor) * -1 + "";
                    //avisa para não limpar o visor
                    limpaVisor = false;
                break;

                case ",":
                    // processa a parte decimal do numero do visor
                    if (!visor.Contains(',')) visor += ',';
                    //avisa para não limpar o visor
                    limpaVisor = false;
                break;

                case "+":
                case "-":
                case "x":
                case ":":
                case "=":
                    // processar as operaçoes aritméticas
                    if (!string.IsNullOrEmpty(operador))
                    {
                        //recuperar os dados anteriormente guardados
                        double operando1 = Convert.ToDouble(primeiroOperando);
                        double operando2 = Convert.ToDouble(visor);

                        // efetuar o cálculo escolhido
                        switch (operador)
                        {
                            case "+":
                                visor = operando1 + operando2 + "";
                            break;
                            case "-":
                                visor = operando1 - operando2 + "";
                            break;
                            case "x":
                                visor = operando1 * operando2 + "";
                            break;
                            case ":":
                                visor = operando1 / operando2 + "";
                            break;

                        }
                    }
                    //guardar o operador para aproxima operação
                    if (bt == "=") operador = "";
                    else
                        operador = bt;
                    //guarda o visor como primeiro operando
                    primeiroOperando = visor;
                    // limpa o visor
                    limpaVisor = true;

                break;

                //reiniciar calculadora
                case "C":
                    visor = "0";
                    operador = "";
                    primeiroOperando = "";
                    limpaVisor = true;
                break;

            }



            //enviar dados para View
            ViewBag.Visor = visor;
            ViewBag.Operador = operador;
            ViewBag.PrimeiroOperando = primeiroOperando;
            ViewBag.LimpaVisor = limpaVisor + "";
            return View();
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
