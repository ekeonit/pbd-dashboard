using System;
using System.Web.Mvc;

namespace PBP.TwitterHud.Web.Controllers
{
    public class PBPTweetsController : Controller
    {
        public PBPTweetsController(IGetPBPTweetsService getPBPTweetsService)
        {
            throw new System.NotImplementedException();
        }

        public JsonResult Get(DateTime sinceDateTime)
        {
            throw new NotImplementedException();
        }
    }
}