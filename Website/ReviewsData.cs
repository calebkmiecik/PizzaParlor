using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Website;

public static class ReviewsData
{
    private const string FilePath = "reviews.json";

    public static List<Review> GetReviews()
    {
        if (File.Exists(FilePath))
        {
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Review>>(json);
        }

        return new List<Review>();
    }

    public static void AddReview(Review review)
    {
        var reviews = GetReviews();
        reviews.Insert(0, review);
        SaveReviews(reviews);
    }

    private static void SaveReviews(List<Review> reviews)
    {
        var json = JsonSerializer.Serialize(reviews);
        File.WriteAllText(FilePath, json);
    }
}
