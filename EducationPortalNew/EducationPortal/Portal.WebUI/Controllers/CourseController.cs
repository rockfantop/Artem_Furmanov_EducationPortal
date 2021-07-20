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
using System.Threading.Tasks;

namespace Portal.WebUI.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICourseService courseService;
        private readonly ICourseSkillService courseSkillService;
        private readonly IMaterialsService materialsService;
        private readonly UserManager<User> userManager;

        public CourseController(IMapper mapper,
            ICourseService courseService,
            ICourseSkillService courseSkillService,
            IMaterialsService materialsService,
            UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.courseService = courseService;
            this.courseSkillService = courseSkillService;
            this.materialsService = materialsService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> ShowCourses(int? page)
        {
            if (page == null)
            {
                page = 1;
            }

            var result = await this.courseService.GetPublicListAsync(Convert.ToInt32(page), 10);

            var listOfCourses = this.mapper.Map<PagedListModel<CourseViewModel>>(result.Result);

            return this.View(listOfCourses);
        }

        [HttpGet]
        public async Task<IActionResult> CourseDetails(Guid id)
        {
            var courseResult = await this.courseService.GetCourseAsync(id);

            var course = this.mapper.Map<CourseViewModel>(courseResult.Result);

            var skillListResult = await this.courseSkillService.GetCourseSkillsListAsync(1, 10, id);

            var skillList = this.mapper.Map<PagedListModel<SkillViewModel>>(skillListResult.Result);

            var skillsAndCourse = new SkillAndCourseViewModel
            {
                Course = course,
                Skills = skillList
            };

            return this.View(skillsAndCourse);
        }

        [HttpGet]
        public async Task<IActionResult> Subscribe(Guid id)
        {
            var courseResult = await this.courseService.SubscribeOnCourseAsync(Guid.Parse(this.userManager.GetUserId(HttpContext.User)), id);

            if (!courseResult.IsSuccesful)
            {
                ModelState.AddModelError(String.Empty, courseResult.Message);

                return View();
            }

            return RedirectToAction("ShowCourses", "Course");
        }

        [HttpGet]
        public async Task<IActionResult> ShowSubscribedCourses(int? page)
        {
            if (page == null)
            {
                page = 1;
            }

            var result = await this.courseService.GetSubscribedCoursesAsync(Guid.Parse(this.userManager.GetUserId(HttpContext.User)), Convert.ToInt32(page));

            var listOfCourses = this.mapper.Map<PagedListModel<CourseViewModel>>(result.Result);

            return this.View(listOfCourses);
        }

        [HttpGet]
        public async Task<IActionResult> SubscribedCourse(Guid id)
        {
            var result = await this.courseService.GetCourseAsync(id);

            var course = this.mapper.Map<CourseViewModel>(result.Result);

            var skillListResult = await this.courseSkillService.GetCourseSkillsListAsync(1, 10, id);

            var skillList = this.mapper.Map<PagedListModel<SkillViewModel>>(skillListResult.Result);

            var courseAndSkill = new SkillAndCourseViewModel
            {
                Course = course,
                Skills = skillList
            };

            return this.View(courseAndSkill);
        }

        [HttpGet]
        public async Task<IActionResult> FinishCourse(Guid id)
        {
            await this.courseService.FinishCourse(id, (Guid.Parse(this.userManager.GetUserId(HttpContext.User))));

            return RedirectToAction("ShowSubscribedCourses", "Course");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
