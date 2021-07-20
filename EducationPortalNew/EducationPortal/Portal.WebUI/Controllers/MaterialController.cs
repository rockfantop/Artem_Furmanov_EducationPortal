using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using Portal.Domain.Identity;
using Portal.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.WebUI.Controllers
{
    [Authorize]
    public class MaterialController : Controller
    {
        private readonly IMapper mapper;
        private readonly IMaterialsService materialsService;
        private readonly ICourseService courseService;
        private readonly UserManager<User> userManager;

        public MaterialController(IMapper mapper, IMaterialsService materialsService, ICourseService courseService, UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.materialsService = materialsService;
            this.courseService = courseService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult AddTextMaterial(Guid courseId)
        {
            return this.View(new TextMaterialViewModel { Id = courseId });
        }

        [HttpPost]
        public async Task<IActionResult> AddTextMaterial(Guid id, [FromForm] TextMaterialViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var textMaterialDTO = this.mapper.Map<TextMaterialDTO>(model);

            textMaterialDTO.Id = Guid.NewGuid();
            textMaterialDTO.CourseId = id;
            textMaterialDTO.DateOfPublication = DateTime.Now.Date;

            var result = await this.materialsService.TextMaterialService.AddTextMaterialAsync(textMaterialDTO);

            if (!result.IsSuccesful)
            {
                ModelState.AddModelError(String.Empty, result.Message);
            }

            return RedirectToAction("EditCourse", "MyCourse", new { id = textMaterialDTO.CourseId });
        }

        [HttpGet]
        public IActionResult AddVideoMaterial(Guid courseId)
        {
            return this.View(new VideoMaterialViewModel { Id = courseId });
        }

        [HttpPost]
        public async Task<IActionResult> AddVideoMaterial(Guid id, [FromForm] VideoMaterialViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var videoMaterialDTO = this.mapper.Map<VideoMaterialDTO>(model);

            videoMaterialDTO.Id = Guid.NewGuid();
            videoMaterialDTO.CourseId = id;
            videoMaterialDTO.DateOfPublication = DateTime.Now.Date;

            var result = await this.materialsService.VideoMaterialService.AddVideoMaterialAsync(videoMaterialDTO);

            if (!result.IsSuccesful)
            {
                ModelState.AddModelError(String.Empty, result.Message);
            }

            return RedirectToAction("EditCourse", "MyCourse", new { id = videoMaterialDTO.CourseId });
        }

        [HttpGet]
        public IActionResult AddInternetMaterial(Guid courseId)
        {
            return this.View(new InternetMaterialViewModel { Id = courseId });
        }

        [HttpPost]
        public async Task<IActionResult> AddInternetMaterial(Guid id, [FromForm] InternetMaterialViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var internetMaterialDTO = this.mapper.Map<InternetMaterialDTO>(model);

            internetMaterialDTO.Id = Guid.NewGuid();
            internetMaterialDTO.CourseId = id;
            internetMaterialDTO.DateOfPublication = DateTime.Now.Date;

            var result = await this.materialsService.InternetMaterialService.AddInternetMaterialAsync(internetMaterialDTO);

            if (!result.IsSuccesful)
            {
                ModelState.AddModelError(String.Empty, result.Message);
            }

            return RedirectToAction("EditCourse", "MyCourse", new { id = internetMaterialDTO.CourseId });
        }

        [HttpGet]
        public async Task<IActionResult> ShowMaterial(Guid courseId, Guid materialId)
        {
            ViewBag.MaterialId = courseId;

            var result = await this.materialsService.GetMaterialAsync(materialId, Guid.Parse(this.userManager.GetUserId(HttpContext.User)));

            var viewMaterial = this.mapper.Map<MaterialViewModel>(result.Result);

            return this.View(viewMaterial);
        }

        [HttpGet]
        public async Task<IActionResult> Pass(Guid courseId, Guid materialId)
        {
            var result = await this.materialsService.PassMaterialAsync(materialId, Guid.Parse(this.userManager.GetUserId(HttpContext.User)));

            return RedirectToAction("SubscribedCourse", "Course", new { id = courseId });
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
