using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cemet.Models;
using Cemet.Repository;

namespace Cemet.Controllers
{
    public class ItemController : Controller
    {
        //
        // GET: /Item/

        public ActionResult Index()
        {
            List<ViewModelItem> items = new List<ViewModelItem>();
            using (var db = new CemetEntities())
            {
                var results = db.Item.ToList();

                foreach (var item in results)
                {
                    var satuanName = db.Satuan.SingleOrDefault(c => c.SatuanId == item.Satuan_SatuanId).Nama;
                    items.Add(new ViewModelItem() { ItemId = item.ItemId, Barcode = item.Barcode, KodeSendiri = item.KodeSendiri, Nama = item.Nama, Satuan_SatuanId = item.Satuan_SatuanId, SatuanNama = satuanName });
                }
            }
            return View(items);
        }

        //
        // GET: /Item/Details/5

        public ActionResult Details(int id)
        {           
            return View();
        }

        //
        // GET: /Item/Create

        private List<SelectListItem> PrepareSelectList()
        {
            List<SelectListItem> satuan = new List<SelectListItem>();
            using (var db = new CemetEntities())
            {
                var satuanResults = db.Satuan.ToList();
                foreach (var item in satuanResults)
                {
                    satuan.Add(new SelectListItem() { Text = item.Nama, Value = item.SatuanId.ToString() });
                }
            }

            return satuan;
        }

        public ActionResult Create()
        {
            ViewData["Satuan"] = PrepareSelectList();
            return View(new ViewModelItem());
        }

        //
        // POST: /Item/Create

        [HttpPost]
        public ActionResult Create(ViewModelItem item)
        {
            try
            {
                using (var db = new CemetEntities())
                {
                    Item itemToBeSaved = new Item();
                    itemToBeSaved.Nama = item.Nama;
                    itemToBeSaved.KodeSendiri = item.KodeSendiri;
                    itemToBeSaved.Satuan_SatuanId = item.Satuan_SatuanId;
                    itemToBeSaved.Barcode = item.Barcode;
                    db.Item.Add(itemToBeSaved);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Item/Edit/5

        public ActionResult Edit(int id)
        {
            ViewModelItem item = new ViewModelItem();
            using (var db = new CemetEntities())
            {
                var result = db.Item.SingleOrDefault(c => c.ItemId == id);
                item.Nama = result.Nama;
                item.KodeSendiri = result.KodeSendiri;
                item.Satuan_SatuanId = result.Satuan_SatuanId;
                item.Barcode = result.Barcode;
                item.ItemId = result.ItemId;
            }

            ViewData["Satuan"] = PrepareSelectList();
            return View(item);
        }

        //
        // POST: /Item/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, ViewModelItem model)
        {
            try
            {
                using (var db = new CemetEntities())
                {
                    var result = db.Item.SingleOrDefault(c => c.ItemId == id);
                    result.Nama = model.Nama;
                    result.KodeSendiri = model.KodeSendiri;
                    result.Satuan_SatuanId = model.Satuan_SatuanId;
                    result.Barcode = model.Barcode;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Item/Delete/5

        public ActionResult Delete(int id)
        {
            using (var db = new CemetEntities())
            {
                var item = db.Item.SingleOrDefault(c => c.ItemId == id);
                db.Item.Remove(item);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
