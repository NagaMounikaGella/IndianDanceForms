using IndianDanceForms.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IndianDanceForms.Controllers
{
    public class HomeController : Controller
    {
        public DanceFormsEntities _danceFormsDatabase = new DanceFormsEntities();
        // GET: Home
        public ActionResult Index(string danceFormType,string origin)
        {
            //dance form type search - setting up the dropdown list
            var danceFormTypeList = new List<string>();
            var danceFormTypeQuery = from dFT in _danceFormsDatabase.DanceFormsDetails
                             orderby dFT.DanceFormType
                             select dFT.DanceFormType;//Brings all dance form types and order by it of each family

            danceFormTypeList.AddRange(danceFormTypeQuery.Distinct());/*Brings only 1 dance form type 
                                                            of each family to display in drop down list*/
            ViewBag.danceFormType = new SelectList(danceFormTypeList);


            /*Display List(sending all dance forms to the view)*/
            var myDanceForms = from dF in _danceFormsDatabase.DanceFormsDetails select dF;

            //title search
            if (!String.IsNullOrEmpty(origin))
            {
                myDanceForms = myDanceForms.Where(dF => dF.Origin.Contains(origin));
            }

            //last bit of genre search
            if (!String.IsNullOrEmpty(danceFormType))
            {
                myDanceForms = myDanceForms.Where(dF => dF.DanceFormType == danceFormType);

            }

            return View(myDanceForms);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] DanceFormsDetail danceForm)
        {
            if(ModelState.IsValid)
            {
                _danceFormsDatabase.DanceFormsDetails.Add(danceForm);
                _danceFormsDatabase.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Liked(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanceFormsDetail danceForm = _danceFormsDatabase.DanceFormsDetails.Find(id);

            if (danceForm == null)
            {
                return HttpNotFound();
            }
            if(danceForm.LikesNo == null)
            {
                danceForm.LikesNo = 0;
            }
            danceForm.LikesNo = danceForm.LikesNo + 1;

            if (ModelState.IsValid)
            {
                _danceFormsDatabase.Entry(danceForm).State = EntityState.Modified;
                _danceFormsDatabase.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult DisLiked(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanceFormsDetail danceForm = _danceFormsDatabase.DanceFormsDetails.Find(id);

            if (danceForm == null)
            {
                return HttpNotFound();
            }
            if (danceForm.DisLikesNo == null)
            {
                danceForm.DisLikesNo = 0;
            }
            danceForm.DisLikesNo = danceForm.DisLikesNo + 1;

            if(ModelState.IsValid)
            {
                _danceFormsDatabase.Entry(danceForm).State = EntityState.Modified;
                _danceFormsDatabase.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanceFormsDetail danceForm = _danceFormsDatabase.DanceFormsDetails.Find(id);
            if(danceForm == null)
            {
                return HttpNotFound();
            }
            

            return View(danceForm);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanceFormsDetail danceForm = _danceFormsDatabase.DanceFormsDetails.Find(id);
            if(danceForm == null)
            {
                return HttpNotFound();
            }

            return View(danceForm);
        }

        [HttpPost]
        public ActionResult Edit(DanceFormsDetail danceForm)
        {
            if(ModelState.IsValid)
            {
                _danceFormsDatabase.Entry(danceForm).State = EntityState.Modified;
                _danceFormsDatabase.SaveChanges();
                return RedirectToAction("Details/" + danceForm.Id);
            }

            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanceFormsDetail danceForm = _danceFormsDatabase.DanceFormsDetails.Find(id);
            if(danceForm == null)
            {
                return HttpNotFound();
            }
            return View(danceForm);
        }

        [HttpPost]
        public ActionResult Delete(DanceFormsDetail danceForm)
        {
            if (ModelState.IsValid)
            {
                _danceFormsDatabase.Entry(danceForm).State = EntityState.Modified;
                _danceFormsDatabase.DanceFormsDetails.Remove(danceForm);
                _danceFormsDatabase.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        
    }
}