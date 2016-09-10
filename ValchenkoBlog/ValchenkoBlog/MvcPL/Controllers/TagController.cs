using System.Linq;
using System.Web.Mvc;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models.Tag;

namespace MvcPL.Controllers
{
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

        //GET-запрос к методу Delete несет потенциальную уязвимость!
        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            TagEntity tag = tagService.GetById(id);

            if (tag == null)
                return HttpNotFound();

            return View(tag.ToMvcTag());
        }

        //Post/Redirect/Get (PRG) — модель поведения веб-приложений, используемая
        //разработчиками для защиты от повторной отправки данных веб-форм
        //(Double Submit Problem)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(TagEntity tag)
        {
            tagService.Delete(tag);
            return RedirectToAction("Index");
        }

        private readonly ITagService tagService;
    }
}