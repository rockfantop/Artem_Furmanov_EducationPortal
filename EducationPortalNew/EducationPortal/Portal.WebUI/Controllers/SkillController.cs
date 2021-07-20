using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using Portal.WebUI.Models;
using Portal.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.WebUI.Controllers
{
    [Authorize]
    public class SkillController : Controller
    {
        private readonly ICourseSkillService courseSkillService;
        private readonly IMapper mapper;

        public SkillController(ICourseSkillService courseSkillService, IMapper mapper)
        {
            this.courseSkillService = courseSkillService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] SkillViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            model.Id = Guid.NewGuid();

            var courseSkill = this.mapper.Map<CourseSkillDTO>(model);

            var result = await this.courseSkillService.AddSkillAsync(courseSkill);

            if (!result.IsSuccesful)
            {
                ModelState.AddModelError(String.Empty, result.Message);

                return this.View(model);
            }

            return RedirectToAction("Get", "Skill", new { id = courseSkill.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await this.courseSkillService.GetSkill(id);

            var skill = new SkillViewModel
            {
                Title = result.Result.Title,
                Description = result.Result.Description
            };

            return this.View(skill);
        }

        [HttpGet]
        public async Task<IActionResult> Show(int? page)
        {
            if (page == null)
            {
                page = 1;
            }

            var result = await this.courseSkillService.GetListAsync(Convert.ToInt32(page), 10);

            var listOfSkill = this.mapper.Map<PagedListModel<SkillViewModel>>(result.Result);

            return this.View(listOfSkill);
        }

        public IActionResult Index()
        {
            return this.View();
        }
    }
}
