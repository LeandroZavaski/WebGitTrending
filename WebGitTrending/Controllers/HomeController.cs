using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebGitTrending.Context;
using WebGitTrending.Models;
using static WebGitTrending.Models.TrendingModel;

namespace WebGitTrending.Controllers
{
    public class HomeController : Controller
    {
        private TrendingContext contexto = new TrendingContext();

        public async Task<ActionResult> Index()
        {
            List<Item> result = await contexto.Item.ToListAsync();
            return View(result);
        }

        public ActionResult ReadGit()
        {
            var currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            var lastDate = DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd");

            var url = "https://api.github.com/search/repositories?q=pushed:" + lastDate + ".." + currentDate + "&sort=stars&order=desc";

            HttpWebRequest webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            if (webRequest != null)
            {
                webRequest.Method = "GET";
                webRequest.UserAgent = "Find-GitHubRepository-App";
                webRequest.Accept = "application/vnd.github.v3+json";
                webRequest.ServicePoint.Expect100Continue = false;

                try
                {
                    RootObject obj = new RootObject();

                    using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream(), Encoding.UTF8))
                    {
                        string reader = responseReader.ReadToEnd();
                        obj = JsonConvert.DeserializeObject<RootObject>(reader);
                    }

                    //Limpar Tabelas para uma nova inserção
                    DeleteData();

                    //Inserir novos registro nas tabelas
                    InsertData(obj);

                }
                catch (Exception e)
                {
                    ViewBag.Message = "Não foi possível efetuar a leitura da API." + e.Message;
                }
            }

            return RedirectToAction("Index");
        }

        private void DeleteData()
        {
            try
            {
                using (var ctx = new TrendingContext())
                {
                    IQueryable<Item> allItems = ctx.Item;
                    ctx.Item.RemoveRange(allItems);
                    ctx.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Item', RESEED, 0)");
                    ctx.SaveChanges();

                    IQueryable<RootObject> allRoot = ctx.RootObject;
                    ctx.RootObject.RemoveRange(allRoot);
                    ctx.Database.ExecuteSqlCommand("DBCC CHECKIDENT('RootObject', RESEED, 0)");
                    ctx.SaveChanges();

                    IQueryable<Owner> allOwner = ctx.Owner;
                    ctx.Owner.RemoveRange(allOwner);
                    ctx.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Owner', RESEED, 0)");
                    ctx.SaveChanges();

                }
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
            }
        }

        private void InsertData(RootObject obj)
        {
            try
            {
                using (contexto)
                {
                    contexto.RootObject.Add(obj);
                    contexto.SaveChanges();
                }

            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
            }
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}