using BusinessLogicLayer.Services.Interface;
using BusinessLogicLayer.Models;
using System.Diagnostics;
using Entity_Framwork.Models;
using Microsoft.AspNetCore.Mvc;

namespace Entity_Framwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        private readonly IVoteService _voteService;
        private readonly ICategoryService _categoryService;

        public HomeController(ILogger<HomeController> logger, 
            IProjectService projectService, 
            IUserService userService,
            ICommentService commentService,
            IVoteService voteService,
            ICategoryService categoryService)
        {
            _logger = logger;

            _projectService = projectService;
            _userService = userService;
            _commentService = commentService;
            _voteService = voteService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("project")]
        public async Task<IActionResult> GetAllProjectAsync()
        {
            var project = await _projectService.GetAllProject();
            return Ok(project);
        }

        [HttpGet("project/page")]
        public async Task<IActionResult> GetAllProjectWithPage(int pageNumber, int pageSize)
        {
            var project = await _projectService.GetAllProjectWithPageAsync(pageNumber, pageSize);
            return Ok(project);
        }

        [HttpGet("project/{id}")]
        public async Task<IActionResult> GetProjectById(Guid id)
        {
            Console.WriteLine(id);
            var project = await _projectService.GetByIdAsync(id);

            if (project == null) return NotFound();

            return Ok(project);
        }

        [HttpPost("project")]
        public async Task<IActionResult> CreateProjectAsync([FromBody] ProjectModel model)
        {
            var project = await _projectService.CreateAsync(model);
            return Ok(project);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetAllUser()
        {
            var alluser = await _userService.GetAllUserAsync();
            return Ok(alluser);
        }

        [HttpGet("category")]
        public async Task<IActionResult> GetAllCategory()
        {
            var allCategory = await _categoryService.GetAllCategory();
            return Ok(allCategory);
        }

        [HttpPost("comment")]
        public async Task<IActionResult> CreateCommentAsync([FromBody] CommentModel model)
        {
            var comment = await _commentService.CreateCommentAsync(model);
            return Ok(comment);
        }

        [HttpPost("vote")]
        public async Task<IActionResult> AddVoteAsync([FromBody] VoteModel model)
        {
            var vote = await _voteService.AddVoteAsync(model);
            return Ok(vote);
        }

        public IActionResult Privacy()
        {
             return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
