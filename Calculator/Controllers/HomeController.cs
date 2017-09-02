using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Calculator.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Result = "";
            ViewBag.Operation = "";
            ViewBag.History = "";
            return View();
        }

        [HttpPost]
        public ActionResult Operation(string submit, string History)
        {
            string finalresult = "";
            string result = "";
            string oper = "";
            string textboxValue = Request.Form["text"];

            if (submit == "=" && submit != "C")
            {
                if (textboxValue != null)
                {
                    Match oMatch = Regex.Match(textboxValue, @"[*+/-]");

                    if (oMatch.Success)
                    {
                        try
                        {
                            double data = Convert.ToDouble(new DataTable().Compute(textboxValue, null));
                            finalresult = data.ToString();
                        }
                        catch (Exception)
                        {
                            finalresult = "Syntax Error";
                        }
                    }
                }
            }
            else if (submit != "=" && submit == "C")
            {
                result = "";
            }
            else if (submit != "=" && submit == "M+")
            {
                if (History == null || History == "")
                {
                    History = textboxValue;
                    result = "";
                }
                else
                {
                    var x = double.Parse(textboxValue.Replace("'", ""));
                    var h = double.Parse(History.Replace("'", ""));
                    double d = x + h;
                    History = d.ToString();
                    result = "";
                }
            }
            else if (submit != "=" && submit == "M-")
            {
                if (History == null || History == "")
                {
                    History = textboxValue;
                    result = "";
                }
                else
                {
                    var x = double.Parse(textboxValue.Replace("'", ""));
                    var h = double.Parse(History.Replace("'", ""));
                    double d = h - x;
                    History = d.ToString();
                    result = "";
                }
            }
            else if (submit != "=" && submit == "MR")
            {
                if (History == null || History == "")
                {
                    finalresult = "";
                    result = "";
                }
                else
                {
                    finalresult = History;
                    result = "";
                }
            }
            else if (submit != "=" && submit == "MC")
            {
                History = "";
            }
            else if (submit == "<--")
            {
                result = textboxValue.Substring(0, textboxValue.Length - 1);
            }
            else
            {
                result += submit;
            }

            if (finalresult != null && finalresult != "")
            {
                oper = finalresult;
            }
            else
            {
                if (textboxValue != null && textboxValue != "" && submit != "M-" && submit != "M+" && submit != "C" && submit != "<--")
                {
                    textboxValue += result;
                }
                else
                    textboxValue = result;
            }

            ViewBag.Result = textboxValue;
            ViewBag.Final = oper == null ? "" : oper;
            ViewBag.History = History;

            return View("index");
        }

       
    }
}