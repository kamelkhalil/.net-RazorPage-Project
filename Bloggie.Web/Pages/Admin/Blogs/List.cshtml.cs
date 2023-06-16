using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Pages.Admin.Blogs
{
    public class ListModel : PageModel
    {
        private readonly BloggieDbContext bloggieDbContext;
        public List<BlogPost> BlogPosts { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; } // Property for search term

        public ListModel(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        public async Task OnGet()
        {
            IQueryable<BlogPost> query = bloggieDbContext.BlogPosts;

            // Apply search filter if search term is provided
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(blogPost => blogPost.Heading.Contains(SearchTerm));
            }

            BlogPosts = await query.ToListAsync();
        }
    }

}
