using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Interfaces;
using Portal.Application.ModelsDTO;
using Portal.Domain.Identity;
using Portal.WebUI.Models;
using Portal.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Portal.WebUI.Controllers
{
    [Authorize]
    public class MyCourseController : Controller
    {
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly ICourseService courseService;
        private readonly ICourseSkillService courseSkillService;

        public MyCourseController(IMapper mapper, UserManager<User> userManager, ICourseService courseService, ICourseSkillService courseSkillService)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.courseService = courseService;
            this.courseSkillService = courseSkillService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult AddCourse()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse([FromForm] CourseCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            model.Id = Guid.NewGuid();
            model.OwnerId = Guid.Parse(this.userManager.GetUserId(HttpContext.User));

            var emptyCourseDTO = this.mapper.Map<EmptyCourseDTO>(model);

            var result = await this.courseService.CreateCourseAsync(emptyCourseDTO);


            if (!result.IsSuccesful)
            {
                ModelState.AddModelError(String.Empty, result.Message);

                return this.View(model);
            }

            return RedirectToAction("Index", "MyCourse");
        }

        [HttpGet]
        public async Task<IActionResult> ShowMyCourses(int? page)
        {
            if (page == null)
            {
                page = 1;
            }

            var result = await this.courseService.GetUserOwnedCourseListAsync(Guid.Parse(this.userManager.GetUserId(HttpContext.User)), Convert.ToInt32(page), 10);

            var listOfCourses = this.mapper.Map<PagedListModel<CourseViewModel>>(result.Result);

            return this.View(listOfCourses);
        }

        [HttpGet]
        public async Task<IActionResult> EditCourse(Guid id)
        {
            ViewBag.ReturnUrl = id;

            var courseResult = await this.courseService.GetCourseAsync(id);

            var course = this.mapper.Map<CourseViewModel>(courseResult.Result);

            var skillListResult = await this.courseSkillService.GetCourseSkillsListAsync(1, 10, id);

            var skillList = this.mapper.Map<PagedListModel<SkillViewModel>>(skillListResult.Result);

            var skillsAndCourse = new SkillAndCourseViewModel
            {
                Course = course,
                Skills = skillList
            };

            if ((Guid.Parse(this.userManager.GetUserId(HttpContext.User)) == course.OwnerId))
            {
                return this.View(skillsAndCourse);
            }

            return RedirectToAction("ShowMyCourses", "MyCourse");
        }

        [HttpGet]
        public async Task<IActionResult> AddSkill(Guid id, int? page)
        {
            if (page == null)
            {
                page = 1;
            }

            ViewBag.ReturnUrl = id;

            var result = await this.courseSkillService.GetNonCourseSkillsListAsync(Convert.ToInt32(page), 10, id);

            var listOfSkill = this.mapper.Map<PagedListModel<SkillViewModel>>(result.Result);

            return this.View(listOfSkill);
        }

        [HttpGet]
        public async Task<IActionResult> AddSkillToCourse(Guid courseId, Guid skillId)
        {
            var result = await this.courseService.AddCourseSkillToCourseAsync(skillId, courseId);

            if (!result.IsSuccesful)
            {
                ModelState.AddModelError(String.Empty, result.Message);

                return this.View();
            }

            return RedirectToAction("EditCourse", "MyCourse", new { id = courseId });
        }

        [HttpGet]
        public async Task<IActionResult> Publish(Guid id)
        {
            var result = await this.courseService.GetCourseAsync(id);

            result.Result.Materials = null;
            result.Result.IsPublic = true;

            var updateResult = await this.courseService.UpdateCourseAsync(result.Result);

            return RedirectToAction("ShowMyCourses", "MyCourse");
        }
    }
}
