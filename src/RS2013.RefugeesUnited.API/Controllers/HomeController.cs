﻿using System.Threading.Tasks;
using System.Web.Mvc;
using RS2013.RefugeesUnited.Model;
using RS2013.RefugeesUnited.Model.RefugeesUnited;
using RS2013.RefugeesUnited.Services;
using RS2013.RefugeesUnited.Model;

namespace RS2013.RefugeesUnited.API.Controllers
{
	public class HomeController : Controller
	{
		private ISessionService SessionService { get; set; }
		private IRefugeesUnitedService RefugeesUnitedService { get; set; }

		public HomeController(ISessionService sessionService, IRefugeesUnitedService refugeesUnitedService)
		{
			SessionService = sessionService;
			RefugeesUnitedService = refugeesUnitedService;
		}

		public async Task<ActionResult> Index()
		{
				//Test Data
			var testProfile = new Profile
			{
				username = "",
				givenName = "kaelanc",
				surName = "fouwelsc",
				password = "1234",
				otherInformation = "I like trains",
				lastSighting = "Test"
			};
			var testDevice = new Device { Number = "+447842073150" };
				//Test Data

			var testUsername = await RefugeesUnitedService.GenerateUsername(testProfile.givenName, testProfile.surName);
				testProfile.username = testUsername;
				//Get API to generate a Username

			var testUserExists = await RefugeesUnitedService.UserExists(testProfile.username);
				//Check if user exists

			//var testRegister = await RefugeesUnitedService.Register(testDevice, testProfile); // todo <-- Problematic
				//Attempt to register said user

			var testLogin = await RefugeesUnitedService.Login(testDevice, "kaelanc.fouwelsc", "1234");
				//Atempt to login said user

			var testSearch = await RefugeesUnitedService.Search(testProfile);
				//Search for said user

			var testLogout = await RefugeesUnitedService.Logout("kaelanc.fouwelsc");
				//Attempt to logout said user
			
			//Break here
			return Content("");
		}

		[HttpPost]
		public async Task<ActionResult> GlobalUSSD(
			string subscriber, string protocol,
			string sadsSmsMessage = null, string sadsListSegment = null,
			string sadsTextSegment = null, string sadsErrorMessage = null)
		{
			var connector = Request.Headers["X-Connector"]; // wap/sip/http/smpp
			var connectorDescription = Request.Headers["X-Connector-Description"];
			var userAgent = Request.UserAgent;
			var host = Request.Headers["Host"];
			var contentType = Request.ContentType;
			var ussdMessage = Request.Headers["WHOISD-USSD-MESSAGE"]; // USSD user reply
			var ussdAddress = Request.Headers["WHOISD-USSD-ADDRESS"];
			var cookie = Request.Cookies; // To identify session
			var locationCountry = Request.Headers["SADS-Location-Country-Name"];
			var locationCity = Request.Headers["SADS-Location-City-Name"];
			var locationCountryId = Request.Headers["SADS-Location-Country-Id"];
			var locationCityId = Request.Headers["SADS-Location-City-Id"];
			var locationVLR = Request.Headers["SADS-Location-VLR"];
			var locationLatitude = Request.Headers["SADS-Location-Latitude"];
			var locationLongitude = Request.Headers["SADS-Location-Longitude"];
			var locationTimezone = Request.Headers["SADS-Location-Timezone"];

			var myResponse = "";
			var mySession = new Session();

			mySession.State = SessionState.MainMenu; //todo <-- get from request.cookies?

			switch (mySession.State)
			{
				case SessionState.Terminated:
					myResponse = 
						"Your session has been terminated. Please start again";
					break;
				case SessionState.AuthenticationOptions:
					myResponse =
						"[AuthOptions]\n)";
					break;
				case SessionState.ConnectAccount:
					myResponse =
						"[Connect Account]\n)";
					break;
				case SessionState.Register:
					myResponse =
						"[Register]\n)";
					break;
				case SessionState.Login:
					myResponse =
						"[Login]\nEnter choice\n1)Login with login code\n2) Connect account - choose this if first login from device";
					break;
				case SessionState.LoginCode:
					myResponse =
						"Enter your login code";
					break;
				case SessionState.MainMenu:
					myResponse = 
						"[Main Menu]\nEnter choice\n1) Login\n2) Register\n3) Connect Account";
					break;
				default:
					myResponse = 
						"You somehow broke the session state, have a cookie";
					break;
			}
			return Content("");
		}
	}
}
