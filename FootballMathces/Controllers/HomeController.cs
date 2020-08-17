using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FootballMathces.Models;

namespace FootballMathces.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IFootballMatchesRepository _footballMatchesRepository;
		public HomeController(ILogger<HomeController> logger,IFootballMatchesRepository footballMatchesRepository)
		{
			_logger = logger;
			_footballMatchesRepository = footballMatchesRepository;
		}

		public IActionResult Index()
		{
			var model = _footballMatchesRepository.Teams;
			return View(model);
		}

		public IActionResult Create()
		{
			var milan = new Team{Name = "Milan", Desc="Nesto" };



			_footballMatchesRepository.Add(milan);

			_footballMatchesRepository.SaveChanges();

			return RedirectToAction(nameof(Index));
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
