using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Website.Pages
{
    public class ReviewsModel : PageModel
    {
        [BindProperty]
        public string NewReviewText { get; set; }

        public IActionResult OnGet()
        {
            ViewData["Reviews"] = ReviewsData.GetReviews();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrWhiteSpace(NewReviewText))
            {
                var newReview = new Review
                {
                    Text = NewReviewText,
                    DateAdded = DateTime.Now
                };

                ReviewsData.AddReview(newReview);
            }

            return RedirectToPage();
        }
    }
}
