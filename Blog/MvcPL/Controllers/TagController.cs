using System.Linq;
using System.Web.Mvc;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models.Tag;

namespace MvcPL.Controllers
{
    [Authorize(Roles = "admin")]
    public class TagController : Controller
    {
        public TagController(ITagService tagService)
        {
            this.tagService = tagService;
        }

        public ActionResult Index() => View(tagService.GetAll().Select(tag => tag.ToMvcTag()));

        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTagViewModel createTagViewModel)
        {
            tagService.Create(createTagViewModel.ToBllTag());
            return RedirectToAction("Index");
        }

        #region Edit
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("BadRequest", "Error");

            EditTagViewModel model = tagService.GetById((int)id)?.ToMvcEditTag();

            if (model == null)
                return RedirectToAction("NotFound", "Error");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditTagViewModel model)
        {
            tagService.Update(model.ToBllTag());
            return RedirectToAction("Index");
        }
        #endregion

        //GET-запрос к методу Delete несет потенциальную уязвимость!
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("BadRequest", "Error");

            var tag = tagService.GetById((int)id)?.ToMvcTag();

            if (tag == null)
                return RedirectToAction("NotFound", "Error");

            return View(tag);
        }

        //Post/Redirect/Get (PRG) — модель поведения веб-приложений, используемая
        //разработчиками для защиты от повторной отправки данных веб-форм
        //(Double Submit Problem)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tagService.Delete(tagService.GetById(id));
            return RedirectToAction("Index");
        }

        private readonly IService<TagEntity> tagService;
    }
}